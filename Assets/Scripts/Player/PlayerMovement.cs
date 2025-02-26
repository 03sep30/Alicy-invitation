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

    public bool isJumping;
    public bool isDashing;
    public bool isBreaking;
    public bool fanAvailable = false;

    private Animator animator;
    private Rigidbody rb;
    private PlayerHealth playerHealth;
    private PlayerStamina playerStamina;
    private PlayerController playerController;
    private CameraController cam;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerHealth = GetComponent<PlayerHealth>();
        playerStamina = GetComponent<PlayerStamina>();
        playerController = GetComponent<PlayerController>();
        animator = GetComponent<Animator>();
        cam = Camera.main.GetComponent<CameraController>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        playerController.currentSize = CharacterSize.Normal;
        Physics.gravity = new Vector3(0, -25f, 0);
    }

    void Update()
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
    }

    void Move()
    {
        Vector3 moveDir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;

        if (moveDir.magnitude >= 0.1f)
        {
            Vector3 cameraDir = cam.GetCameraForward();
            Vector3 moveDirection = cameraDir * moveDir.z + cam.transform.right * moveDir.x;

            rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.deltaTime);

            animator.SetInteger("State", (int)CharacterState.Walking);
        }
        else
        {
            animator.SetInteger("State", (int)CharacterState.Idle);
        }
    }

    void Jump()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (playerController.currentSize == CharacterSize.Normal && !isJumping)
            {
                if (fanAvailable)
                {
                    rb.AddForce(Vector3.up * (jumpForce * 1.5f), ForceMode.Impulse);
                    fanAvailable = false;
                }
                else
                {
                    rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                }
                isJumping = true;
            }
        }
    }

    public void Dash()
    {
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetInteger("State", (int)CharacterState.Running);
            Vector3 dashDirection = transform.forward;
            rb.velocity = dashDirection * dashSpeed;
            playerStamina.DecreaseStamina(1);

            if (playerController.currentSize == CharacterSize.Big && !isBreaking)
            {
                isBreaking = true;
            }
        }
    }
}