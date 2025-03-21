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

    public GameObject playerBody;
    public bool isGrounded = false;
    public float lastGroundedY;
    public bool enterPortal = false;
    public bool crushing = false;
    [SerializeField] private float deathFallHeight = 75f;
    public StatusEffect currentEffect;

    public AudioSource audioSource;
    public float luckyBoxTime = 5f;

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
        if (!crushing)
        {
            playerBody.transform.localScale = Vector3.one;
        }
    }

    void DropCalculation()
    {
        float fallDistance = lastGroundedY - transform.position.y;
        if (fallDistance >= deathFallHeight && !playerHealth.isDie && !enterPortal)
        {
            playerHealth.Die();
            //lastGroundedY = playerHealth.SpawnPoint.transform.position.y;
        }
    }

    void OnControllerColliderHit(ControllerColliderHit coll)
    {
        
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
        
        if (coll.gameObject.CompareTag("LuckyBox"))
        {
            if (currentEffect != null)
            {
                currentEffect.RemoveEffect();
                currentEffect = null;
            }
            var luckyBox = coll.gameObject.GetComponent<LuckyBox>();
            luckyBox.OpenLuckyBox();
            currentEffect = luckyBox.currentStatus;
            StartCoroutine(currentEffect.EffectTime());
            StartCoroutine(LuckyBoxTime(coll.gameObject));
        }

        
    }

    private IEnumerator LuckyBoxTime(GameObject luckyBox)
    {
        luckyBox.SetActive(false);
        yield return new WaitForSeconds(luckyBoxTime);
        luckyBox.SetActive(true);
    }

    private void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.CompareTag("ParryingObj"))
        {
            Debug.Log($"{coll.gameObject.name}");
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
        if (other.gameObject.CompareTag("CrushBlock"))
        {
            crushing = true;
            characterController.enabled = false;
            playerBody.transform.localScale = new Vector3(1f, 0.2f, 1f);

            playerHealth.Die();
        }

        if (other.gameObject.CompareTag("Portal"))
        {
            var portal = other.GetComponent<Portal>();
            Transform targetPortal = portal.EnterPortal();

            if (targetPortal != null)
            {
                characterController.enabled = false;
                transform.position = targetPortal.position;
                lastGroundedY = targetPortal.position.y;
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
