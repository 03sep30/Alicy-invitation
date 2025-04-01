using StarterAssets;
using UnityEngine;
using UnityEngine.UI;

enum CharacterState
{
    Idle,
    Walking,
    Running,
    Dead
}

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 10f;
    public float jumpForce = 7f;  // 점프 힘
    public float gravityMultiplier = 2f; // 상승 중 중력 가속도
    public float fallMultiplier = 2.5f;  // 낙하 중 중력 가속도

    [Header("Ground Check")]
    public Transform groundCheckPoint;
    public float groundCheckRadius = 0.3f;
    public LayerMask groundLayer;  // 바닥 레이어

    private Rigidbody rb;
    public bool isGrounded;
    public bool isJumping;

    public GameObject blindnessPanel;
    private RawImage blindnessRawImage;
    private RectTransform blindnessRectTransform;

    public float dashSpeed = 2f;

    public bool isDashing;
    public bool isBreaking;
    public bool isWalking;
    public bool isBoosting;

    public float originalMoveSpeed;
    public float originalJumpHeight;
    public float originalSprintSpeed;

    private PlayerHealth playerHealth;
    private PlayerStamina playerStamina;
    private PlayerController playerController;
    private ThirdPersonController thirdPersonController;

    [Header("BGM")]
    public AudioClip breakingSound;

    // timeout deltatime
    private float _jumpTimeoutDelta;
    private float _fallTimeoutDelta;

    public float JumpTimeout = 0.50f;

    public float FallTimeout = 0.15f;

    // animation IDs
    private int _animIDSpeed;
    private int _animIDGrounded;
    private int _animIDJump;
    private int _animIDFreeFall;
    private int _animIDMotionSpeed;

    private Animator _animator;

    private bool _hasAnimator;

    private CameraController cam;

    void Start()
    {
        //cam = FindObjectOfType<CameraController>();
        //_hasAnimator = TryGetComponent(out _animator);
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;

        //rb = GetComponent<Rigidbody>();

        //_jumpTimeoutDelta = JumpTimeout;
        //_fallTimeoutDelta = FallTimeout;

        playerHealth = GetComponentInChildren<PlayerHealth>();
        playerStamina = GetComponent<PlayerStamina>();
        playerController = GetComponent<PlayerController>();
        thirdPersonController = GetComponent<ThirdPersonController>();

        blindnessRawImage = blindnessPanel.GetComponent<RawImage>();
        blindnessRectTransform = blindnessPanel.GetComponent<RectTransform>();

        originalMoveSpeed = thirdPersonController.MoveSpeed;
        originalJumpHeight = thirdPersonController.JumpHeight;
        originalSprintSpeed = thirdPersonController.SprintSpeed;
    }

    void Update()
    {
        //GroundedCheck();
        //AssignAnimationIDs();

        if (!playerHealth.isDie && playerController.currentSize == CharacterSize.Small)
        {
            gameObject.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            //Move();
            thirdPersonController.Move();
            Boost();

            blindnessRectTransform.sizeDelta = new Vector2(1920f, 1080f);
        }
        if (!playerHealth.isDie && !playerController.crushing && playerController.currentSize == CharacterSize.Normal)
        {
            thirdPersonController.MoveSpeed = originalMoveSpeed;
            thirdPersonController.JumpHeight = originalJumpHeight;

            gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
            //Move();
            //Jump();
            thirdPersonController.Move();
            thirdPersonController.JumpAndGravity();

            blindnessRectTransform.sizeDelta = new Vector2(1920f, 1080f);
        }
        if (!playerHealth.isDie && playerController.currentSize == CharacterSize.Big)
        {
            //thirdPersonController.MoveSpeed = originalMoveSpeed;
            //thirdPersonController.JumpHeight = originalJumpHeight;

            gameObject.transform.localScale = new Vector3(2f, 2f, 2f);
            //Move();
            thirdPersonController.Move();
            blindnessRectTransform.sizeDelta = new Vector2(2800f, 1600f);

            Dash();
        }
    }

    //private void AssignAnimationIDs()
    //{
    //    _animIDSpeed = Animator.StringToHash("Speed");
    //    _animIDGrounded = Animator.StringToHash("Grounded");
    //    _animIDJump = Animator.StringToHash("Jump");
    //    _animIDFreeFall = Animator.StringToHash("FreeFall");
    //    _animIDMotionSpeed = Animator.StringToHash("MotionSpeed");
    //}

    //private void Move()
    //{
    //    Vector3 moveDir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;

    //    Vector3 cameraDir = cam.GetCameraForward();
    //    Vector3 moveDirection = (cameraDir * moveDir.z + cam.transform.right * moveDir.x).normalized;

    //    if (moveDirection.magnitude > 0)
    //    {
    //        Vector3 move = moveDirection * moveSpeed * Time.deltaTime;
    //        rb.MovePosition(rb.position + move);
    //    }

    //    if (_hasAnimator)
    //    {
            
    //    }
    //}

    //private void GroundedCheck()
    //{
    //    isGrounded = Physics.CheckSphere(groundCheckPoint.position, groundCheckRadius, groundLayer);
    //    if (_hasAnimator)
    //    {
    //        _animator.SetBool(_animIDGrounded, isGrounded);
    //    }
    //}

    //private void Jump()
    //{
    //    if (isGrounded)
    //    {
    //        // 낙하 타이머 초기화
    //        _fallTimeoutDelta = FallTimeout;

    //        if (_hasAnimator)
    //        {
    //            _animator.SetBool(_animIDJump, false);
    //            _animator.SetBool(_animIDFreeFall, false);
    //        }

    //        // 바닥에 닿으면 속도를 리셋 (약간 내려가게 설정)
    //        if (rb.velocity.y < 0.0f)
    //        {
    //            rb.velocity = new Vector3(rb.velocity.x, -2f, rb.velocity.z);
    //        }

    //        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
    //        {
    //            isJumping = true;
    //            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
    //            Debug.Log("점프 시작");

    //            if (_hasAnimator)
    //            {
    //                _animator.SetBool(_animIDJump, true);
    //            }
    //        }
    //    }
    //    else
    //    {
    //        // 낙하 타이머 감소
    //        if (_fallTimeoutDelta > 0.0f)
    //        {
    //            _fallTimeoutDelta -= Time.deltaTime;
    //        }
    //        else
    //        {
    //            if (_hasAnimator)
    //            {
    //                _animator.SetBool(_animIDFreeFall, true);
    //            }
    //        }

    //        // 바닥에 닿기 전까지 점프 입력을 비활성화
    //        Input.ResetInputAxes();
    //    }

    //    // 중력 적용
    //    ApplyGravity();
    //}

    // 중력 적용 함수
    //private void ApplyGravity()
    //{
    //    if (!isGrounded)
    //    {
    //        float gravityScale = rb.velocity.y < 0 ? fallMultiplier : gravityMultiplier;
    //        rb.velocity += Vector3.up * Physics.gravity.y * (gravityScale - 1) * Time.deltaTime;

    //        Debug.Log(rb.velocity.y < 0 ? "낙하 중" : "점프 중");
    //    }
    //    else
    //    {
    //        isJumping = false;

    //        if (_hasAnimator)
    //        {
    //            _animator.SetBool(_animIDJump, false);
    //            _animator.SetBool(_animIDFreeFall, false);
    //        }
    //    }
    //}


    public void Dash()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //playerStamina.DecreaseStamina(1);

            if (playerController.currentSize == CharacterSize.Big && !isBreaking)
            {
                isBreaking = true;
                Debug.Log("Break");
            }
        }
    }

    public void Boost()
    {
        float boostSpeed = 30f;
        float jumpForce = 5f;

        thirdPersonController.MoveSpeed = boostSpeed;
        thirdPersonController.JumpHeight = jumpForce;

        isBoosting = true;
    }
}