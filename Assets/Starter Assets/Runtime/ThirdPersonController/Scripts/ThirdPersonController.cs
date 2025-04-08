﻿using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

/* Note: animations are called via the controller for both the character and capsule using animator null checks
 */

namespace StarterAssets
{
    [RequireComponent(typeof(Rigidbody))] // CharacterController 대신 Rigidbody 요구
#if ENABLE_INPUT_SYSTEM
    [RequireComponent(typeof(PlayerInput))]
#endif
    public class ThirdPersonController : MonoBehaviour
    {
        public float fallSpeed = 0.99f;

        [Header("Player")]
        [Tooltip("Move speed of the character in m/s")]
        public float MoveSpeed = 2.0f;

        [Tooltip("Sprint speed of the character in m/s")]
        public float SprintSpeed = 5.335f;

        [Tooltip("How fast the character turns to face movement direction")]
        [Range(0.0f, 0.3f)]
        public float RotationSmoothTime = 0.12f;

        [Tooltip("Acceleration and deceleration")]
        public float SpeedChangeRate = 10.0f;

        [Tooltip("Additional force applied when jumping")]
        public float JumpForce = 5f;

        public AudioClip LandingAudioClip;
        public AudioClip[] FootstepAudioClips;
        [Range(0, 1)] public float FootstepAudioVolume = 0.5f;

        [Space(10)]
        [Tooltip("The height the player can jump")]
        public float JumpHeight = 1.2f;

        [Tooltip("The character uses its own gravity value. The engine default is -9.81f")]
        public float Gravity = -15.0f;

        [Space(10)]
        [Tooltip("Time required to pass before being able to jump again. Set to 0f to instantly jump again")]
        public float JumpTimeout = 0.50f;

        [Tooltip("Time required to pass before entering the fall state. Useful for walking down stairs")]
        public float FallTimeout = 0.15f;

        [Header("Player Grounded")]
        [Tooltip("If the character is grounded or not")]
        public bool Grounded = true;

        [Tooltip("Useful for rough ground")]
        public float GroundedOffset = 0.1f; // 값 수정

        [Tooltip("The radius of the grounded check")]
        public float GroundedRadius = 0.3f; // 값 수정

        [Tooltip("What layers the character uses as ground")]
        public LayerMask GroundLayers;

        [Header("Physics Settings")]
        [Tooltip("Character's drag when grounded")]
        public float GroundDrag = 5f;

        [Tooltip("Character's drag when in air")]
        public float AirDrag = 1f;

        [Header("Cinemachine")]
        [Tooltip("The follow target set in the Cinemachine Virtual Camera that the camera will follow")]
        public GameObject CinemachineCameraTarget;

        [Tooltip("How far in degrees can you move the camera up")]
        public float TopClamp = 70.0f;

        [Tooltip("How far in degrees can you move the camera down")]
        public float BottomClamp = -30.0f;

        [Tooltip("Additional degress to override the camera. Useful for fine tuning camera position when locked")]
        public float CameraAngleOverride = 0.0f;

        [Tooltip("For locking the camera position on all axis")]
        public bool LockCameraPosition = false;

        public float cameraRotation;

        // cinemachine
        private float _cinemachineTargetYaw;
        private float _cinemachineTargetPitch;

        // player
        public float _speed;
        private float _animationBlend;
        private float _targetRotation = 0.0f;
        private float _rotationVelocity;
        private float _terminalVelocity = 53.0f;

        // 플랫폼 상호작용 변수
        private Transform _currentPlatform;
        private Vector3 _lastPlatformPosition;
        private Vector3 _platformMomentum = Vector3.zero;
        private float _platformMomentumDamping = 0.9f;
        private bool _isJumping = false;

        // timeout deltatime
        private float _jumpTimeoutDelta;
        private float _fallTimeoutDelta;

        // animation IDs
        private int _animIDSpeed;
        private int _animIDGrounded;
        private int _animIDJump;
        private int _animIDFreeFall;
        private int _animIDMotionSpeed;

#if ENABLE_INPUT_SYSTEM
        private PlayerInput _playerInput;
#endif
        private Animator _animator;
        private Rigidbody _rigidbody; // CharacterController 대신 Rigidbody
        private CapsuleCollider _collider; // 캡슐 콜라이더 추가
        private StarterAssetsInputs _input;
        private GameObject _mainCamera;

        private const float _threshold = 0.01f;

        private bool _hasAnimator;
        private Vector3 _moveDirection;


        private bool IsCurrentDeviceMouse
        {
            get
            {
#if ENABLE_INPUT_SYSTEM
                return _playerInput.currentControlScheme == "KeyboardMouse";
#else
				return false;
#endif
            }
        }


        private void Awake()
        {
            // get a reference to our main camera
            if (_mainCamera == null)
            {
                _mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
            }
        }

        private void Start()
        {
            _cinemachineTargetYaw = CinemachineCameraTarget.transform.rotation.eulerAngles.y;

            // 카메라 높이 조정
            Vector3 newPosition = CinemachineCameraTarget.transform.localPosition;
            newPosition.y = 1.3f; // 1.2~1.5 정도로 조정
            newPosition.x = 0.3f; // 0.3~0.5 정도로 조정
            CinemachineCameraTarget.transform.localPosition = newPosition;

            _hasAnimator = TryGetComponent(out _animator);
            _rigidbody = GetComponent<Rigidbody>(); // Rigidbody 가져오기
            _collider = GetComponent<CapsuleCollider>(); // 캡슐 콜라이더 가져오기

            // 리지드바디 설정
            _rigidbody.constraints = RigidbodyConstraints.FreezeRotation; // 회전 제한
            _rigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous; // 연속 충돌 감지
            _rigidbody.interpolation = RigidbodyInterpolation.Interpolate; // 보간 설정

            _input = GetComponent<StarterAssetsInputs>();
#if ENABLE_INPUT_SYSTEM
            _playerInput = GetComponent<PlayerInput>();
#else
			Debug.LogError( "Starter Assets package is missing dependencies. Please use Tools/Starter Assets/Reinstall Dependencies to fix it");
#endif

            AssignAnimationIDs();

            // reset our timeouts on start
            _jumpTimeoutDelta = JumpTimeout;
            _fallTimeoutDelta = FallTimeout;
        }

        private void Update()
        {
            _hasAnimator = TryGetComponent(out _animator);

            GroundedCheck();
            HandleJumpInput();
            UpdateAnimation();
            CameraRotation();
        }

        private void FixedUpdate()
        {
            Move();
            ApplyGravity();
        }

        private void AssignAnimationIDs()
        {
            _animIDSpeed = Animator.StringToHash("Speed");
            _animIDGrounded = Animator.StringToHash("Grounded");
            _animIDJump = Animator.StringToHash("Jump");
            _animIDFreeFall = Animator.StringToHash("FreeFall");
            _animIDMotionSpeed = Animator.StringToHash("MotionSpeed");
        }

        // 호환성을 위한 메서드 추가
        public void JumpAndGravity()
        {
            // 새 컨트롤러에서는 HandleJumpInput()과 ApplyGravity()가 이 역할을 대체
            HandleJumpInput();
            // 필요한 경우 ApplyGravity()도 호출
        }

        // 호환성을 위한 변수 추가 (public)
        public float _verticalVelocity
        {
            get { return _rigidbody.velocity.y; }
            set
            {
                Vector3 vel = _rigidbody.velocity;
                vel.y = value;
                _rigidbody.velocity = vel;
            }
        }



        private void GroundedCheck()
        {
            // 더 정확한 지면 체크 위치 계산
            Vector3 spherePosition = new Vector3(
                transform.position.x,
                transform.position.y - _collider.height / 2 + _collider.radius - 0.05f, // 약간 더 아래로
                transform.position.z
            );

            // 1. 구체 캐스트로 체크
            Grounded = Physics.CheckSphere(spherePosition, GroundedRadius, GroundLayers, QueryTriggerInteraction.Ignore);

            // 2. 레이캐스트로 추가 체크 (더 안정적)
            if (!Grounded)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, Vector3.down, out hit, _collider.height / 2 + 0.2f, GroundLayers, QueryTriggerInteraction.Ignore))
                {
                    Grounded = true;
                }
            }

            // 디버그 목적으로 로그 출력 (필요하면 주석 해제)
            // Debug.Log("Grounded: " + Grounded);

            // 지면에 닿아있으면 드래그 값 증가, 공중에 있으면 감소
            _rigidbody.drag = Grounded ? GroundDrag : AirDrag;

            // 지면에 닿으면 애니메이션 상태 업데이트
            if (Grounded)
            {
                _isJumping = false;

                if (_hasAnimator)
                {
                    _animator.SetBool(_animIDFreeFall, false);
                }
            }
            else
            {
                // 디버그용 - 지면이 아닐 때 체크 포인트 시각화
                // Debug.DrawRay(spherePosition, Vector3.down * 0.1f, Color.red);
            }
        }

        private void CameraRotation()
        {
            // if there is an input and camera position is not fixed
            if (_input.look.sqrMagnitude >= _threshold && !LockCameraPosition)
            {
                //Don't multiply mouse input by Time.deltaTime; 
                float deltaTimeMultiplier = IsCurrentDeviceMouse ? cameraRotation : Time.deltaTime;

                _cinemachineTargetYaw += _input.look.x * deltaTimeMultiplier;
                _cinemachineTargetPitch += _input.look.y * deltaTimeMultiplier;
            }

            // clamp our rotations so our values are limited 360 degrees
            _cinemachineTargetYaw = ClampAngle(_cinemachineTargetYaw, float.MinValue, float.MaxValue);
            _cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, BottomClamp, TopClamp);

            // Cinemachine will follow this target
            CinemachineCameraTarget.transform.rotation = Quaternion.Euler(_cinemachineTargetPitch + CameraAngleOverride,
                _cinemachineTargetYaw, 0.0f);
        }

        public void Move()
        {
            // set target speed based on move speed, sprint speed and if sprint is pressed
            float targetSpeed = _input.sprint ? SprintSpeed : MoveSpeed;

            // if there is no input, set the target speed to 0
            if (_input.move == Vector2.zero) targetSpeed = 0.0f;

            // 현재 속도 계산 (XZ 평면)
            Vector3 currentVelocity = new Vector3(_rigidbody.velocity.x, 0, _rigidbody.velocity.z);
            float currentSpeed = currentVelocity.magnitude;

            // 속도 변화량 계산
            float speedOffset = 0.1f;
            float inputMagnitude = _input.analogMovement ? _input.move.magnitude : 1f;

            // 가속 또는 감속
            if (currentSpeed < targetSpeed - speedOffset || currentSpeed > targetSpeed + speedOffset)
            {
                _speed = Mathf.Lerp(currentSpeed, targetSpeed * inputMagnitude,
                    Time.fixedDeltaTime * SpeedChangeRate);

                _speed = Mathf.Round(_speed * 1000f) / 1000f;
            }
            else
            {
                _speed = targetSpeed;
            }

            // 이동 방향 계산
            Vector3 inputDirection = new Vector3(_input.move.x, 0.0f, _input.move.y).normalized;

            // 입력이 있을 때만 회전
            if (_input.move != Vector2.zero)
            {
                _targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg +
                                  _mainCamera.transform.eulerAngles.y;
                float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation, ref _rotationVelocity,
                    RotationSmoothTime);

                // 카메라 방향으로 회전
                transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
            }

            // 이동 방향 (전방 기준)
            _moveDirection = Quaternion.Euler(0.0f, _targetRotation, 0.0f) * Vector3.forward;

            // 플랫폼 위에 있을 때
            if (_currentPlatform != null)
            {
                // 플랫폼 이동 계산
                Vector3 platformDelta = _currentPlatform.position - _lastPlatformPosition;
                if (platformDelta.magnitude > 0.001f)
                {
                    // 플랫폼 이동에 따른 위치 조정
                    _rigidbody.MovePosition(_rigidbody.position + platformDelta);
                }
                _lastPlatformPosition = _currentPlatform.position;
            }

            // 공중에 있을 때 플랫폼 운동량 적용
            if (!Grounded && _platformMomentum.magnitude > 0.01f)
            {
                // 운동량 적용 및 감쇠
                _rigidbody.AddForce(_platformMomentum, ForceMode.VelocityChange);
                _platformMomentum *= _platformMomentumDamping;
            }

            // 지면에 있을 때만 이동 입력 처리
            if (Grounded)
            {
                // 현재 Y축 속도 유지
                float yVelocity = _rigidbody.velocity.y;

                // 새로운 속도 설정 (이동 + 현재 Y축 속도)
                Vector3 targetVelocity = _moveDirection * _speed + Vector3.up * yVelocity;

                // 리지드바디 속도 설정 (위치 변경 대신)
                _rigidbody.velocity = targetVelocity;
            }
            else
            {
                // 공중에서는 힘을 가해 이동 (더 자연스러운 움직임)
                _rigidbody.AddForce(_moveDirection * _speed * 0.5f, ForceMode.Force);

                // 속도 제한 (너무 빠르게 움직이지 않도록)
                Vector3 horizontalVelocity = new Vector3(_rigidbody.velocity.x, 0, _rigidbody.velocity.z);
                if (horizontalVelocity.magnitude > _speed)
                {
                    Vector3 limitedVelocity = horizontalVelocity.normalized * _speed;
                    _rigidbody.velocity = new Vector3(limitedVelocity.x, _rigidbody.velocity.y, limitedVelocity.z);
                }
            }

            // 애니메이션 블렌드 업데이트
            _animationBlend = Mathf.Lerp(_animationBlend, targetSpeed, Time.fixedDeltaTime * SpeedChangeRate);
            if (_animationBlend < 0.01f) _animationBlend = 0f;
        }

        private void HandleJumpInput()
        {
            if (Grounded)
            {
                _fallTimeoutDelta = FallTimeout;

                if (_input.jump && _jumpTimeoutDelta <= 0.0f)
                {
                    _isJumping = true;

                    // 플랫폼 운동량 저장
                    if (_currentPlatform != null)
                    {
                        // 플랫폼 속도 추정 (이전 프레임과 현재 프레임의 위치 차이)
                        Vector3 platformVelocity = (_currentPlatform.position - _lastPlatformPosition) / Time.deltaTime;
                        _platformMomentum = platformVelocity;
                        _platformMomentum.y = 0; // 수직 운동량 제외
                    }

                    // 플랫폼 참조 제거
                    _currentPlatform = null;

                    // Jump 실행은 FixedUpdate에서 처리
                    _jumpTimeoutDelta = JumpTimeout;
                    Jump();
                }

                if (_jumpTimeoutDelta >= 0.0f)
                {
                    _jumpTimeoutDelta -= Time.deltaTime;
                }
            }
            else
            {
                // reset jump timeout timer
                _jumpTimeoutDelta = JumpTimeout;

                // fall timeout
                if (_fallTimeoutDelta >= 0.0f)
                {
                    _fallTimeoutDelta -= Time.deltaTime;
                }
                else
                {
                    // 낙하 중 애니메이션 설정
                    if (_hasAnimator)
                    {
                        _animator.SetBool(_animIDFreeFall, true);
                    }
                }

                // 공중에서는 점프 불가
                _input.jump = false;
            }
        }

        private void Jump()
        {
            // 현재 수직 속도 초기화
            Vector3 velocity = _rigidbody.velocity;
            velocity.y = 0f;
            _rigidbody.velocity = velocity;

            // 점프 힘 적용
            float jumpForce = Mathf.Sqrt(JumpHeight * -2f * Gravity);
            _rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

            // 애니메이션 설정
            if (_hasAnimator)
            {
                _animator.SetBool(_animIDJump, true);
            }
        }

        private void ApplyGravity()
        {
            // 자체 중력 적용 (Unity의 기본 중력 대신)
            if (!Grounded)
            {
                _rigidbody.AddForce(Vector3.up * Gravity, ForceMode.Acceleration);
            }
        }

        private void UpdateAnimation()
        {
            if (_hasAnimator)
            {
                _animator.SetBool(_animIDGrounded, Grounded);
                _animator.SetFloat(_animIDSpeed, _animationBlend);
                _animator.SetFloat(_animIDMotionSpeed, _input.move.magnitude);

                // 지면 착지 시 점프 애니메이션 해제
                if (Grounded && _animator.GetBool(_animIDJump))
                {
                    _animator.SetBool(_animIDJump, false);
                    _animator.SetBool(_animIDFreeFall, false);
                }
            }
        }

        // 플랫폼 설정 메서드
        public void SetCurrentPlatform(Transform platform)
        {
            _currentPlatform = platform;
            if (platform != null)
            {
                _lastPlatformPosition = platform.position;
            }
            else
            {
                if (!_isJumping)
                {
                    _platformMomentum = Vector3.zero;
                }
            }
        }

        // 점프 상태 확인 메서드
        public bool IsJumping()
        {
            return _isJumping;
        }

        private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
        {
            if (lfAngle < -360f) lfAngle += 360f;
            if (lfAngle > 360f) lfAngle -= 360f;
            return Mathf.Clamp(lfAngle, lfMin, lfMax);
        }

        private void OnFootstep(AnimationEvent animationEvent)
        {
            if (animationEvent.animatorClipInfo.weight > 0.5f)
            {
                if (FootstepAudioClips.Length > 0)
                {
                    var index = Random.Range(0, FootstepAudioClips.Length);
                    AudioSource.PlayClipAtPoint(FootstepAudioClips[index], transform.TransformPoint(_collider.center), FootstepAudioVolume);
                }
            }
        }

        private void OnLand(AnimationEvent animationEvent)
        {
            if (animationEvent.animatorClipInfo.weight > 0.5f)
            {
                AudioSource.PlayClipAtPoint(LandingAudioClip, transform.TransformPoint(_collider.center), FootstepAudioVolume);
            }
        }
    }
}