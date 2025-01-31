using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 10f;
    public float jumpForce = 5f;
    public float mouseSensitivity = 2f;

    public bool isJumping;
    private float lastCameraYaw;
    private float cameraPitch = 0f;

    private Camera cam;
    public Rigidbody rb;
    private PlayerHealth playerHealth;

    void Start()
    {
        cam = Camera.main;
        rb = GetComponent<Rigidbody>();
        playerHealth = GetComponent<PlayerHealth>();
        lastCameraYaw = cam.transform.rotation.eulerAngles.y;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        Move();
        Jump();
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
        }
    }

    void Jump()
    {
        if (!isJumping && Input.GetMouseButtonDown(0))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isJumping = true;
            if (!playerHealth.isDrinkingTeacup)
            playerHealth.TakeDamage(1);
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

    
}