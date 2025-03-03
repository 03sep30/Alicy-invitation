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

        if (playerController.audioSource.clip == walkingSound)
            playerController.audioSource.loop = true;
        else
            playerController.audioSource.loop = false;
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
            isWalking = true;

            
            if (!playerController.audioSource.isPlaying)
            {
                playerController.audioSource.clip = walkingSound;
                playerController.audioSource.Play();
            }
        }
        else
        {
            animator.SetInteger("State", (int)CharacterState.Idle);
            isWalking = false;
            if (playerController.audioSource.isPlaying && playerController.audioSource.clip == walkingSound)
                playerController.audioSource.Stop();
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