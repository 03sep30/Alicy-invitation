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
    public bool fanAvailable = false;

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
            thirdPersonController.Move();
            thirdPersonController.JumpAndGravity();
        }
        if (!playerHealth.isDrinkingTeacup && playerController.currentSize == CharacterSize.Big)
        {
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
            
            if (playerController.currentSize == CharacterSize.Small && !isDashing)
                isDashing = true;
            if (playerController.currentSize == CharacterSize.Big && !isBreaking)
            {
                isBreaking = true;
            }
        }
    }
}