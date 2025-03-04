using UnityEngine;

enum CharacterState
{
    Idle,
    Walking,
    Running,
    Dead
}

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public float dashSpeed = 2f;
    public float bounceForce = 10f;

    public bool isJumping;
    public bool isDashing;
    public bool isBreaking;
    public bool isWalking;
    public bool fanAvailable = false;

    private Animator animator;
    private Rigidbody rb;
    private PlayerHealth playerHealth;
    private PlayerStamina playerStamina;
    private PlayerController playerController;
    private CameraController cam;

    public float fallMultiplier = 2.5f;     // 낙하 시 중력 강화 배수
    public float lowJumpMultiplier = 2f;

    [Header("BGM")]
    public AudioClip jumpingSound;
    public AudioClip walkingSound;
    public AudioClip breakingSound;    

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerHealth = GetComponentInChildren<PlayerHealth>();
        playerStamina = GetComponent<PlayerStamina>();
        playerController = GetComponent<PlayerController>();
        animator = GetComponent<Animator>();
        cam = Camera.main.GetComponent<CameraController>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        playerController.currentSize = CharacterSize.Normal;
        Physics.gravity = new Vector3(0, -25f, 0);
    }

    void FixedUpdate()
    {
        if (!playerHealth.isDrinkingTeacup && playerController.currentSize == CharacterSize.Small)
        {
            Dash();
        }
        if (!playerHealth.isDrinkingTeacup && playerController.currentSize == CharacterSize.Normal)
        {
            Move();
            Jump();
        }
        if (!playerHealth.isDrinkingTeacup && playerController.currentSize == CharacterSize.Big)
        {
            Move();
            Dash();
        }
        else
        {
            Jump();
        }

        if (playerController.audioSource.clip == walkingSound)
            playerController.audioSource.loop = true;
        else
            playerController.audioSource.loop = false;

        if (rb.velocity.y < 0)
        {
            // 플레이어가 낙하 중일 때 추가 중력을 적용하여 빠르게 떨어지게 함
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetMouseButton(0))
        {
            // 상승 중에 점프 버튼을 떼면 상승 감속 효과 적용
            rb.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    void Move()
    {
        // 입력 값 받아오기
        Vector3 moveDir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;

        if (moveDir.magnitude >= 0.1f)
        {
            // 카메라의 전방과 오른쪽 벡터를 이용해 이동 방향 계산
            Vector3 cameraDir = cam.GetCameraForward();
            Vector3 moveDirection = (cameraDir * moveDir.z + cam.transform.right * moveDir.x).normalized;

            // 현재 Rigidbody의 수직 속도는 보존하면서 수평 속도만 업데이트
            Vector3 currentVelocity = rb.velocity;
            Vector3 newVelocity = moveDirection * moveSpeed;
            newVelocity.y = currentVelocity.y;  // 중력이나 점프 등으로 인한 수직 속도 보존

            rb.velocity = newVelocity;

            // 애니메이션 및 사운드 처리
            if (!isJumping)
            {
                animator.SetInteger("State", (int)CharacterState.Walking);
            }
            else
            {
                animator.SetInteger("State", (int)CharacterState.Idle);
            }
                isWalking = true;
            if (!playerController.audioSource.isPlaying)
            {
                playerController.audioSource.clip = walkingSound;
                playerController.audioSource.Play();
            }
        }
        else
        {
            // 입력이 없을 경우, 수평 속도만 0으로 처리 (중력 등은 유지)
            Vector3 newVelocity = rb.velocity;
            newVelocity.x = 0;
            newVelocity.z = 0;
            rb.velocity = newVelocity;

            animator.SetInteger("State", (int)CharacterState.Idle);
            isWalking = false;
            if (playerController.audioSource.isPlaying && playerController.audioSource.clip == walkingSound)
            {
                playerController.audioSource.Stop();
            }
        }
    }

    void Jump()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (playerController.currentSize == CharacterSize.Normal && !isJumping)
            {
                float appliedJumpForce = fanAvailable ? jumpForce * 1.5f : jumpForce;
                // 기존의 수직 속도를 초기화하여 점프를 깔끔하게 시작
                rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
                rb.AddForce(Vector3.up * appliedJumpForce, ForceMode.Impulse);
                isJumping = true;
                fanAvailable = false;

                playerController.audioSource.clip = jumpingSound;
                if (!playerController.audioSource.isPlaying)
                    playerController.audioSource.Play();
            }
        }
    }

    public void Dash()
    {
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetInteger("State", (int)CharacterState.Running);
            Vector3 dashDirection = cam.GetCameraForward();
            rb.velocity = dashDirection * dashSpeed;
            //playerStamina.DecreaseStamina(1);
            
            if (playerController.currentSize == CharacterSize.Small && !isDashing)
                isDashing = true;
            if (playerController.currentSize == CharacterSize.Big && !isBreaking)
            {
                isBreaking = true;
            }
        }
    }

    public void Bounce()
    {
        Vector3 lookDirection = cam.GetCameraForward().normalized;

        rb.velocity = Vector3.zero;
        rb.AddForce(lookDirection * bounceForce, ForceMode.Impulse);
        isDashing = false;
        Debug.Log($"Bounced off wall in direction: {lookDirection}");
    }
}