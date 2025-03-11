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
    }

    void Update()
    {
        if (!playerHealth.isDrinkingTeacup && playerController.currentSize == CharacterSize.Small)
        {
            gameObject.transform.localScale = new Vector3(2.5f, 2.5f, 2.5f);
            thirdPersonController.Move();
            Boost();
        }
        if (!playerHealth.isDrinkingTeacup && playerController.currentSize == CharacterSize.Normal)
        {
            gameObject.transform.localScale = new Vector3(5f, 5f, 5f);
            thirdPersonController.Move();
            thirdPersonController.JumpAndGravity();
        }
        if (!playerHealth.isDrinkingTeacup && playerController.currentSize == CharacterSize.Big)
        {
            gameObject.transform.localScale = new Vector3(10f, 10f, 10f);
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
        thirdPersonController.MoveSpeed = 15f;
        isBoosting = true;
    }
}