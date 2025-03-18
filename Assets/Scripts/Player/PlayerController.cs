using Cinemachine;
using StarterAssets;
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
    private ThirdPersonController thirdPersonController;
    private CharacterController characterController;

    public bool isGrounded = false;
    public float lastGroundedY;
    [SerializeField] private float deathFallHeight = 75f;

    public AudioSource audioSource;

    void Start()
    {
        currentSize = CharacterSize.Normal;
        playerMovement = GetComponent<PlayerMovement>();
        playerHealth = GetComponentInChildren<PlayerHealth>();
        thirdPersonController = GetComponent<ThirdPersonController>();
        audioSource = GetComponent<AudioSource>();
        characterController = GetComponent<CharacterController>();
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
            //lastGroundedY = playerHealth.SpawnPoint.transform.position.y;
        }
    }

    void OnControllerColliderHit(ControllerColliderHit coll)
    {
        //Debug.Log($"{coll.gameObject.name}");
        if (coll.gameObject.CompareTag("Obstacle"))
        {
            playerMovement.isJumping = false;
            lastGroundedY = transform.position.y;
        }
        if (currentSize == CharacterSize.Big && playerMovement.isBreaking && coll.gameObject.CompareTag("Breakable"))
        {
            audioSource.clip = playerMovement.breakingSound;
            audioSource.Play();

            coll.gameObject.GetComponent<ObstacleController>().Explosion();
            playerMovement.isBreaking = false;

        }
        if (coll.gameObject.CompareTag("Ani"))
        {
            Debug.Log("Ani");
            gameObject.transform.parent = coll.gameObject.transform.parent;
        }
        if (coll.gameObject.CompareTag("LuckyBox"))
        {
            coll.gameObject.GetComponent<LuckyBox>().OpenLuckyBox();
            Destroy(coll.gameObject);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            
        }
        if (collision.gameObject.name == "Card.Spade")
        {
            gameObject.transform.parent = null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Portal"))
        {
            var portal = other.GetComponent<Portal>();
            Transform targetPortal = portal.EnterPortal();

            if (targetPortal != null)
            {
                characterController.enabled = false;
                transform.position = targetPortal.position;
                characterController.enabled = true;
            }
        }
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
                    //playerMovement.fanAvailable = true;
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
}
