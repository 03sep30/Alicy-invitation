using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterSize
{
    Small,
    Big,
    Normal,
    None
}

public class PlayerController : MonoBehaviour
{
    public float maxPosition = 0;
    public CharacterSize currentSize;
    
    private PlayerMovement playerMovement;
    private PlayerHealth playerHealth;

    public bool isGrounded = false;
    [SerializeField] private float lastGroundedY;
    [SerializeField] private float deathFallHeight = 75f;

    public AudioSource audioSource;

    void Start()
    {
        currentSize = CharacterSize.Normal;
        playerMovement = GetComponent<PlayerMovement>();
        playerHealth = GetComponentInChildren<PlayerHealth>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        DropCalculation();
    }

    void DropCalculation()
    {
        float fallDistance = lastGroundedY - transform.position.y;
        if (fallDistance >= deathFallHeight && !playerHealth.isDie)
        {
            playerHealth.Die();
            lastGroundedY = playerHealth.SpawnPoint.transform.position.y;
        }
    }

    void OnCollisionEnter(Collision coll)
    {
        Debug.Log($"{coll.gameObject.name}");
        if (coll.gameObject.CompareTag("Obstacle"))
        {
            playerMovement.isJumping = false;
            isGrounded = true;
            lastGroundedY = transform.position.y;
            if (playerMovement.isDashing && currentSize == CharacterSize.Small)
            {
                playerMovement.Bounce();
            }
        }
        if (currentSize == CharacterSize.Big && playerMovement.isBreaking && coll.gameObject.CompareTag("Breakable"))
        {
            audioSource.clip = playerMovement.breakingSound;
            audioSource.Play();

            coll.gameObject.GetComponent<ObstacleController>().Explosion();
            playerMovement.isBreaking = false;

        }
        if (coll.gameObject.name == "Card.Spade")
        {
            gameObject.transform.parent = coll.transform;   
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            //isGrounded = false;
        }
        if (collision.gameObject.name == "Card.Spade")
        {
            gameObject.transform.parent = null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("TTS_Object"))
        {
            other.gameObject.GetComponent<TTSController>().PlayTTS();
        }
        if (other.gameObject.CompareTag("ParryingObj"))
        {
            var parryingObj = other.gameObject.GetComponent<ParryingObj>();

            switch (parryingObj.Name)
            {
                case "Juice":
                    parryingObj.UpdateSize(gameObject);
                    Destroy(other.transform.parent.gameObject);
                    break;
                case "TeaCup":
                    //parryingObj.Invincibility(gameObject);
                    //StartCoroutine(TeaCupTime());
                    Debug.Log("TEACUP");
                    parryingObj.Jump(gameObject);
                    //Destroy(other.transform.gameObject);
                    break;
                case "Raisin":
                    parryingObj.UpdateSize(gameObject);
                    Destroy(other.transform.parent.gameObject);
                    break;
                case "Fan":
                    playerMovement.fanAvailable = true;
                    Destroy(other.transform.parent.gameObject);
                    break;
                default:
                    break;
            }
        }

        if (other.gameObject.CompareTag("SavePoint"))
        {
            playerHealth.SpawnPoint = other.gameObject.transform;
        }
    }

    IEnumerator TeaCupTime()
    {
        float originalJumpForce = playerMovement.jumpForce;
        playerMovement.jumpForce *= 2f;

        yield return new WaitForSeconds(10f);

        playerMovement.jumpForce = originalJumpForce;
        playerHealth.isDrinkingTeacup = false;
    }
}
