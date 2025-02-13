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
    public float rotationSpeed = 10f;
    public float jumpForce = 5f;
    public float mouseSensitivity = 2f;
    public float dashSpeed = 2f;

    public bool isJumping;
    public bool isDashing;
    public bool isBreaking;

    public bool fanAvailable = false;

    private float lastCameraYaw;
    private float cameraPitch = 0f;

    private Animator animator;
    private Camera cam;
    public Rigidbody rb;
    private PlayerHealth playerHealth;
    private PlayerStamina playerStamina;
    private PlayerController playerController;

    void Start()
    {
        cam = Camera.main;

        rb = GetComponent<Rigidbody>();
        playerHealth = GetComponent<PlayerHealth>();
        playerStamina = GetComponent<PlayerStamina>();
        playerController = GetComponent<PlayerController>();
        animator = GetComponent<Animator>();

        lastCameraYaw = cam.transform.rotation.eulerAngles.y;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        playerController.currentSize = CharacterSize.Normal;
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
        UpdateRotation();
    }

    void Move()
    {
        Vector3 moveDir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;

        if (moveDir.magnitude >= 0.1f)
        {
            Vector3 cameraDir = Vector3.Scale(cam.transform.forward, new Vector3(1, 0, 1)).normalized;
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
                if (!playerHealth.isDrinkingTeacup)
                    playerHealth.TakeDamage(1);
            }
        }
    }

    void UpdateRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        cameraPitch -= mouseY;
        cameraPitch = Mathf.Clamp(cameraPitch, -90f, 90f);

        float cameraYaw = cam.transform.eulerAngles.y;
        transform.rotation = Quaternion.Euler(0, cameraYaw, 0);

        cam.transform.localRotation = Quaternion.Euler(cameraPitch, 0, 0);
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