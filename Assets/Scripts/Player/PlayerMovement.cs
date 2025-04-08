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


    void Start()
    {
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
        if (!playerHealth.isDie && playerController.currentSize == CharacterSize.Small)
        {
            gameObject.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            Boost();

            blindnessRectTransform.sizeDelta = new Vector2(1920f, 1080f);
        }
        if (!playerHealth.isDie && !playerController.crushing && playerController.currentSize == CharacterSize.Normal)
        {
            thirdPersonController.MoveSpeed = originalMoveSpeed;
            thirdPersonController.JumpHeight = originalJumpHeight;

            gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
            thirdPersonController.JumpAndGravity();

            blindnessRectTransform.sizeDelta = new Vector2(1920f, 1080f);
        }
        if (!playerHealth.isDie && playerController.currentSize == CharacterSize.Big)
        {
            thirdPersonController.MoveSpeed = originalMoveSpeed;
            thirdPersonController.JumpHeight = originalJumpHeight;

            gameObject.transform.localScale = new Vector3(2f, 2f, 2f);
            blindnessRectTransform.sizeDelta = new Vector2(2800f, 1600f);

            Dash();
        }
    }

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