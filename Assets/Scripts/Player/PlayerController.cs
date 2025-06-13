using StarterAssets;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Drawing;
using Cinemachine;

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
    public GameObject BreakObjectPrefab;

    public float maxPosition = 0;

    public GameObject settingPanel;

    public GameObject playerBody;
    public bool isGrounded = false;
    public float lastGroundedY;
    public bool enterPortal = false;
    public bool crushing = false;
    public float deathFallHeight = 75f;
    
    public float luckyBoxTime = 5f;

    public float playerDamage = 10f;
    public bool isOnStep = false;

    private bool hasKey;
    public bool isPanelActive = false;
    public GameObject KeyImage;
    public GameObject keyPortal;

    public GameObject cheshire;
    public GameObject statusPlane;

    public GameObject bossPanel;
    private string currentBossName;
    private bool isDropDamage;
    public AudioClip[] backgroundBGM;

    public GameObject[] mainCamera;
    public GameObject[] lockCamera;

    [Header("0 Small, 1 Big, 2 SpeedUp, 3 SpeedDown")]
    public Sprite[] statusImages;

    [Header("0 Blue, 1 Orange, 2 Green")]
    public GameObject[] mushroomEffects;

    public AudioSource backgroundAudioSource;
    public StatusEffect currentEffect;
    public AudioSource ttsAudioSource;
    public BossHP bossHP;
    private StarterAssetsInputs _input;
    private PlayerMovement playerMovement;
    private PlayerHealth playerHealth;
    private PlayerUI playerUI;
    private ThirdPersonController thirdPersonController;
    private PlayerTriggerController PlayerTriggerController;
    private PlayerMushroomHandler mushroomHandler;
    private CubePuzzle cubePuzzle;
    private Rigidbody rb;
    private CinemachineVirtualCamera CinemachineVirtualCamera;

    void Start()
    {
        GameManager.Instance.currentSize = CharacterSize.Normal;
        playerMovement = GetComponent<PlayerMovement>();
        playerHealth = GetComponentInChildren<PlayerHealth>();
        thirdPersonController = GetComponent<ThirdPersonController>();
        playerUI = GetComponent<PlayerUI>();
        PlayerTriggerController = GetComponentInChildren<PlayerTriggerController>();
        mushroomHandler = GetComponent<PlayerMushroomHandler>();
        cubePuzzle = FindObjectOfType<CubePuzzle>();
        _input = GetComponent<StarterAssetsInputs>();
        rb = GetComponent<Rigidbody>();
        CinemachineVirtualCamera = FindObjectOfType<CinemachineVirtualCamera>();

        playerHealth.SpawnPoint = playerHealth.startPoint;

        rb.interpolation = RigidbodyInterpolation.None;
        transform.position = playerHealth.SpawnPoint.position;
        rb.interpolation = RigidbodyInterpolation.Interpolate;

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
        if (playerUI != null)
        {
            playerUI.UpdateStatusUI(GameManager.Instance.currentSize);
        }

        if (!isPanelActive)
        {
            thirdPersonController.CameraRotation();
        }

        DropCalculation();
        _input.SetCursorState(!isPanelActive);

        if (!crushing)
        {
            playerBody.transform.localScale = Vector3.one;
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            mushroomHandler.SwapMushroom();
        }

        if (Input.GetMouseButtonDown(0) && !isPanelActive)
        {
            if (!playerHealth.bossStage)
            {
                mushroomHandler.UseMushroom();
            }
            else
            {
                if (currentEffect != null)
                {
                    StartCoroutine(currentEffect.EffectTime());
                    playerUI.BossStageItemImageClear();
                    currentEffect = null;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            settingPanel.gameObject.SetActive(!isPanelActive);
            isPanelActive = settingPanel.activeSelf;
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            SceneManager.LoadScene("Stage2_Boss_Map");
        }
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            transform.position = playerHealth.SpawnPoint.position;
        }
    }

    public void BackgroundBGM(string bossName)
    {
        currentBossName = bossName;
        if (backgroundAudioSource != null)
        {
            backgroundAudioSource.Stop();
            switch (currentBossName)
            {
                case "Stage":
                    if (backgroundAudioSource.clip != backgroundBGM[0])
                    {
                        backgroundAudioSource.clip = backgroundBGM[0];
                        backgroundAudioSource.Play();
                    }
                    break;
                case "GingerCookie":
                    if (backgroundAudioSource.clip != backgroundBGM[1])
                    {
                        backgroundAudioSource.clip = backgroundBGM[1];
                        backgroundAudioSource.Play();
                    }
                    break;
                case "Chef":
                    if (backgroundAudioSource.clip != backgroundBGM[2])
                    {
                        backgroundAudioSource.clip = backgroundBGM[2];
                        backgroundAudioSource.Play();
                    }
                    break;
                case "Cheshire":
                    if (backgroundAudioSource.clip != backgroundBGM[2])
                    {
                        backgroundAudioSource.clip = backgroundBGM[2];
                        backgroundAudioSource.Play();
                    }
                    break;
            }
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
        if (fallDistance >= deathFallHeight && currentBossName == "Chef")
        {
            isDropDamage = true;
            
        }
        if (fallDistance >= deathFallHeight && !playerHealth.isDie && !enterPortal && !isDropDamage)
        {
            playerHealth.Die();
            //lastGroundedY = playerHealth.SpawnPoint.transform.position.y;
        }
        
        if (isDropDamage && thirdPersonController.Grounded)
        {
            playerHealth.TakeDamage(1);
            isDropDamage = false;
        }
    }

    void OnCollisionEnter(Collision coll)
    {
        //Debug.Log(coll.transform.name);

        if (coll.gameObject.CompareTag("BossBody"))
        {
            BossAttack boss = coll.gameObject.GetComponent<BossAttack>();
            playerHealth.TakeDamage(boss.damage);
        }

        if (coll.gameObject.CompareTag("BossPanel"))
        {
            bossPanel.SetActive(true);
            Destroy(coll.gameObject);
        }
        
        if (coll.gameObject.name == "Key")
        {
            GetKey();
        }

        if (coll.gameObject.CompareTag("Boss") && !thirdPersonController.Grounded)
        {
            if (bossHP != null)
            {
                bossHP.DecreaseBossHP(playerDamage);
                Debug.Log("Player Attack");
            }
        }

        if (coll.gameObject.layer == 7)
        {
            lastGroundedY = transform.position.y;
        }

        if (GameManager.Instance.currentSize == CharacterSize.Big && /*playerMovement.isBreaking && */coll.gameObject.CompareTag("Breakable"))
        {
            GameObject breakObject = Instantiate(BreakObjectPrefab, null);
            breakObject.transform.position = coll.gameObject.transform.position;
            Destroy(coll.gameObject);

            DestroyObject destroyObject = breakObject.GetComponent<DestroyObject>();
            if (destroyObject != null)
                destroyObject.StartDestroy(breakObject, 1.5f);

            //audioSource.clip = playerMovement.breakingSound;
            //audioSource.Play();

            //coll.gameObject.GetComponent<ObstacleController>().Explosion();
            //playerMovement.isBreaking = false;
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
            playerUI.BossStageItemImageUpdate(currentEffect.effectID);
            
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

    private IEnumerator mushroomEffectTime(GameObject effect)
    {
        yield return new WaitForSeconds(0.5f);
        effect.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("StageName"))
        {
            var stageNameObj = other.GetComponent<TriggerObject>();
            stageNameObj.UpdateStageNameUI();
        }

        if (other.gameObject.CompareTag("CameraLockPoint"))
        {
            thirdPersonController.LockCameraPosition = true;
            thirdPersonController._cinemachineTargetYaw = 180f;
            thirdPersonController._cinemachineTargetPitch = 24f;
            thirdPersonController.CinemachineCameraTarget.transform.rotation = Quaternion.Euler(0f, 180f, 0f);

            var camFollow = CinemachineVirtualCamera.GetCinemachineComponent<Cinemachine3rdPersonFollow>();
            if (camFollow != null)
            {
                camFollow.CameraDistance = 14f;
            }
        }

        if (other.gameObject.CompareTag("MainCameraPoint"))
        {
            thirdPersonController.LockCameraPosition = false;

            var camFollow = CinemachineVirtualCamera.GetCinemachineComponent<Cinemachine3rdPersonFollow>();
            if (camFollow != null)
            {
                camFollow.CameraDistance = 7f;
            }
        }

        if (other.gameObject.CompareTag("Ghost"))
        {
            PlayerTriggerController.TriggerDust();
            Debug.Log("Dust");
        }

        if (other.gameObject.CompareTag("Mushroom"))
        {
            Mushroom mushroom = other.gameObject.GetComponent<Mushroom>();

            if (mushroom.mushroomType == MushroomType.Blue)
            {
                GameManager.Instance.blueMushroomCount++;
                if (mushroomEffects[0].activeInHierarchy == true)
                    mushroomEffects[0].SetActive(false);
                mushroomEffects[0].SetActive(true);
                StartCoroutine(mushroomEffectTime(mushroomEffects[0]));
            }
            if (mushroom.mushroomType == MushroomType.Orange)
            {
                GameManager.Instance.orangeMushroomCount++;
                if (mushroomEffects[1].activeInHierarchy == true)
                    mushroomEffects[1].SetActive(false);
                mushroomEffects[1].SetActive(true);
                StartCoroutine(mushroomEffectTime(mushroomEffects[1]));
            }
            if (mushroom.mushroomType == MushroomType.Green)
            {
                GameManager.Instance.greenMushroomCount++;
                if (mushroomEffects[2].activeInHierarchy == true)
                    mushroomEffects[2].SetActive(false);
                mushroomEffects[2].SetActive(true);
                StartCoroutine(mushroomEffectTime(mushroomEffects[2]));
            }
            mushroomHandler.GetMushroom();
            Destroy(other.gameObject);
        }

        if (other.gameObject.name == "BossDestroyBlock")
        {
            bossHP = FindObjectOfType<BossHP>();
            if (bossHP != null)
            {
                Destroy(bossHP.gameObject);
            }
        }

        if (other.gameObject.CompareTag("BossAttack"))
        {
            Debug.Log("bossAttack");
            BossAttack bossAttack = other.gameObject.GetComponent<BossAttack>();
            if (bossAttack != null)
            {
                bossAttack.Attack(playerHealth);
                bossAttack.hit = true;
            }
        }

        if (other.gameObject.CompareTag("Dust"))
        {
            PlayerTriggerController.TriggerDust();
            Debug.Log("Dust");
        }

        if (other.gameObject.CompareTag("Mine"))
        {
            var mine = other.GetComponent<MineController>();
            mine.GhostRun();
            mine.GetPuzzle();
            mine.MinePlayerHeal();
            mine.MineGetKey();
            Destroy(mine.gameObject);
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

        if (other.CompareTag("Cheshire"))
        {
            Destroy(other.gameObject);
            cheshire.SetActive(true);
            Debug.Log("Cheshire");
        }

        if (other.gameObject.CompareTag("Portal"))
        {
            var portal = other.GetComponent<Portal>();
            Transform targetPortal = portal.EnterPortal();

            if (targetPortal != null)
            {
                rb.interpolation = RigidbodyInterpolation.None;
                transform.position = targetPortal.position;
                rb.interpolation = RigidbodyInterpolation.Interpolate;
                lastGroundedY = targetPortal.position.y;
            }
        }

        if (other.gameObject.CompareTag("Text_Object"))
        {
            TextController textObj = other.gameObject.GetComponent<TextController>();
            //ttsObj.PlayTTS();
            textObj.StartTextDisplay();
        }
        if (other.gameObject.CompareTag("TTS_Object"))
        {
            TTSController ttsObj = other.gameObject.GetComponent<TTSController>();
            ttsObj.PlayTTS();
        }

        if (other.gameObject.CompareTag("SavePoint"))
        {
            playerHealth.SpawnPoint = other.gameObject.transform;
        }
    }

    public void GetKey()
    {
        if (!hasKey)
        {
            hasKey = true;
            KeyImage.SetActive(true);
            if (keyPortal != null)
                keyPortal.SetActive(true);
        }
    }
}
