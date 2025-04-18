using Cinemachine;
using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public enum CharacterSize
{
    Small,
    Big,
    Normal,
    None
}

public class PlayerController : MonoBehaviour
{
    //public UniversalRendererData rendererData;
    //private ScriptableRendererFeature blitFeature;

    public int orangeMushroomCount = 0;
    public int blueMushroomCount = 0;
    public MushroomType currentMushroom;

    public float maxPosition = 0;
    public CharacterSize currentSize;
    

    public GameObject playerBody;
    public bool isGrounded = false;
    public float lastGroundedY;
    public bool enterPortal = false;
    public bool crushing = false;
    [SerializeField] private float deathFallHeight = 75f;
    
    public float luckyBoxTime = 5f;

    public float playerDamage = 10f;

    private bool hasKey;
    public GameObject KeyImage;

    public GameObject statusPlane;

    public GameObject bossPanel;

    [Header("0 Small, 1 Big, 2 SpeedUp, 3 SpeedDown")]
    public Sprite[] statusImages;
    public StatusEffect currentEffect;

    public AudioSource audioSource;

    private BossHP boss;
    private StarterAssetsInputs _input;
    private PlayerMovement playerMovement;
    private PlayerHealth playerHealth;
    private ThirdPersonController thirdPersonController;
    private PlayerTriggerController PlayerTriggerController;
    private PlayerMushroomHandler mushroomHandler;
    private CubePuzzle cubePuzzle;

    void Start()
    {
        currentSize = CharacterSize.Normal;
        playerMovement = GetComponent<PlayerMovement>();
        playerHealth = GetComponentInChildren<PlayerHealth>();
        thirdPersonController = GetComponent<ThirdPersonController>();
        audioSource = GetComponent<AudioSource>();
        boss = FindObjectOfType<BossHP>();
        PlayerTriggerController = GetComponentInChildren<PlayerTriggerController>();
        mushroomHandler = GetComponent<PlayerMushroomHandler>();
        cubePuzzle = FindObjectOfType<CubePuzzle>();

        currentMushroom = MushroomType.Blue;

        playerHealth.SpawnPoint = playerHealth.startPoint;
        gameObject.transform.position = playerHealth.SpawnPoint.position;

        //_input = GetComponent<StarterAssetsInputs>();

        //foreach (var feature in rendererData.rendererFeatures)
        //{
        //    if (feature.name == "Blit")
        //    {
        //        blitFeature = feature;
        //        break;
        //    }
        //}
    }


    void Update()
    {
        DropCalculation();
        if (!crushing)
        {
            playerBody.transform.localScale = Vector3.one;
        }
        
        //if (_input.sprint && thirdPersonController._speed >= thirdPersonController.SprintSpeed)
        //{
        //    blitFeature.SetActive(true);
        //}
        //else
        //{
        //    blitFeature.SetActive(false);
        //}

        if (Input.GetKeyDown(KeyCode.F))
        {
            mushroomHandler.SwapMushroom();
        }
        if (Input.GetMouseButtonDown(0))
        {
            mushroomHandler.UseMushroom();
        }
    }

    public void UpdateStatus(int num)
    {
        statusPlane.SetActive(true);

        Image plane = statusPlane.GetComponent<Image>();
        if (num <= statusImages.Length)
        {
            plane.sprite = statusImages[num];
        }
        else
        {
            statusPlane.SetActive(false);
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

    void OnCollisionEnter(Collision coll)
    {
        //Debug.Log(coll.transform.name);

        if (coll.gameObject.CompareTag("Mushroom"))
        {
            Mushroom mushroom = coll.gameObject.GetComponent<Mushroom>();
            mushroomHandler.GetMushroom(mushroom);
            Debug.Log("버ㅓ");
        }

        if (coll.gameObject.CompareTag("GingerBoss"))
        {
            playerHealth.TakeDamage(playerHealth.currentHeartHP);
        }

        if (coll.gameObject.CompareTag("BossPanel"))
        {
            bossPanel.SetActive(true);
            Destroy(coll.gameObject);
        }
        
        if (coll.gameObject.name == "Key")
        {
            if (!hasKey)
            {
                hasKey = true;
                KeyImage.SetActive(true);
                Destroy(coll.gameObject);
            }
        }
        if (coll.gameObject.CompareTag("Boss") && !thirdPersonController.Grounded)
        {
            if (boss != null)
            {
                boss.DecreaseBossHP(playerDamage);
                Debug.Log("Player Attack");
            }
        }
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
        if (coll.gameObject.CompareTag("TerrainGround"))
        {
            if (!playerHealth.isDie)
            {
                playerHealth.Die();
            }
        }
        if (coll.gameObject.CompareTag("OvenDoor"))
        {
            if (hasKey)
            {
                var loadScene = coll.gameObject.GetComponent<LoadSceneObj>();
                SceneManager.LoadScene(loadScene.sceneName);
            }
        } 
        if (coll.gameObject.CompareTag("BossMap"))
        {
            var loadScene = coll.gameObject.GetComponent<LoadSceneObj>();
            SceneManager.LoadScene(loadScene.sceneName);
        }
    }

    private IEnumerator LuckyBoxTime(GameObject luckyBox)
    {
        luckyBox.SetActive(false);
        yield return new WaitForSeconds(luckyBoxTime);
        luckyBox.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Dust"))
        {
            PlayerTriggerController.TriggerDust();
            Debug.Log("dust");
        }
        if (other.CompareTag("DeadBlock"))
        {
            playerHealth.Die();
        }

        if (other.CompareTag("CubePuzzle"))
        {
            cubePuzzle.ToggleCube(other.gameObject);
        }
        if (other.transform.name == "PuzzleButton")
        {
            cubePuzzle.CheckPuzzle();

        }
        if (other.gameObject.CompareTag("CrushBlock"))
        {
            crushing = true;
            playerBody.transform.localScale = new Vector3(1f, 0.2f, 1f);

            playerHealth.Die();
        }

        if (other.gameObject.CompareTag("Portal"))
        {
            var portal = other.GetComponent<Portal>();
            Transform targetPortal = portal.EnterPortal();

            if (targetPortal != null)
            {
                transform.position = targetPortal.position;
                lastGroundedY = targetPortal.position.y;
            }
        }
        if (other.gameObject.CompareTag("TTS_Object"))
        {
            Debug.Log("Trigger : TTS_Object");
            TTSController ttsObj = other.gameObject.GetComponent<TTSController>();
            //ttsObj.PlayTTS();
            ttsObj.StartTextDisplay();
        }
        //if (other.gameObject.CompareTag("ParryingObj"))
        //{
        //    var parryingObj = other.gameObject.GetComponent<ParryingObj>();

        //    switch (parryingObj.Name)
        //    {
        //        case "Juice":
        //            parryingObj.UpdateSize(gameObject);
        //            Destroy(other.transform.parent.gameObject);
        //            break;
        //        case "TeaCup":
        //            //parryingObj.Invincibility(gameObject);
        //            //StartCoroutine(TeaCupTime());
        //            Debug.Log("TEACUP");
        //            parryingObj.Jump(gameObject);
        //            //Destroy(other.transform.gameObject);
        //            break;
        //        case "Raisin":
        //            parryingObj.UpdateSize(gameObject);
        //            Destroy(other.transform.parent.gameObject);
        //            break;
        //        case "Fan":
        //            //playerMovement.fanAvailable = true;
        //            Destroy(other.transform.parent.gameObject);
        //            break;
        //        default:
        //            break;
        //    }
        //}

        if (other.gameObject.CompareTag("SavePoint"))
        {
            playerHealth.SpawnPoint = other.gameObject.transform;
        }
        
    }
}
