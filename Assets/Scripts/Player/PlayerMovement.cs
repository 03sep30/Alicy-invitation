using StarterAssets;
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
    public float dashSpeed = 2f;

    public bool isJumping;
    public bool isDashing;
    public bool isBreaking;
    public bool isWalking;
    public bool isBoosting;

    public float originalMoveSpeed;
    public float originalJumpHeight;

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

        originalMoveSpeed = thirdPersonController.MoveSpeed;
        originalJumpHeight = thirdPersonController.JumpHeight;
    }

    void Update()
    {
        if (!playerHealth.isDrinkingTeacup && playerController.currentSize == CharacterSize.Small)
        {
            gameObject.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            thirdPersonController.Move();
            Boost();
        }
        if (!playerHealth.isDrinkingTeacup && playerController.currentSize == CharacterSize.Normal)
        {
            //thirdPersonController.MoveSpeed = originalMoveSpeed;
            //thirdPersonController.JumpHeight = originalJumpHeight;

            gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
            thirdPersonController.Move();
            thirdPersonController.JumpAndGravity();
        }
        if (!playerHealth.isDrinkingTeacup && playerController.currentSize == CharacterSize.Big)
        {
            //thirdPersonController.MoveSpeed = originalMoveSpeed;
            //thirdPersonController.JumpHeight = originalJumpHeight;

            gameObject.transform.localScale = new Vector3(2f, 2f, 2f);
            thirdPersonController.Move();
            Dash();
        }
        else
        {
            thirdPersonController.JumpAndGravity();
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