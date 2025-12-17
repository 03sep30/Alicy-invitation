using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class BossHP : MonoBehaviour
{
    public string bossName;
    public float maxTimeHP;
    public float currentTimeHP;
    public bool isHit = false;
    public bool isPaused = false;
    public bool nextScene = false;
    public string sceneName;
    
    public float hitTime;
    private float currentHitTime;
    public Slider timeHPSlider;
    public TextMeshProUGUI timeHPText;
    public GameObject bossPanel;

    [SerializeField] protected PlayerController playerController;
    [SerializeField] private PlayerHealth playerHealth;

    void Start()
    {
        if (bossName == "Cheshire")
        {
            StartBoss();
        }
    }

    void Update()
    {
        if (!playerHealth.bossStage)
            return;
        if (!isPaused && currentTimeHP > 0)
        {
            currentTimeHP -= Time.deltaTime;
            timeHPSlider.value = currentTimeHP / maxTimeHP;

            if (currentTimeHP <= 3.1f)
            {
                timeHPText.gameObject.SetActive(true);
                timeHPText.text = currentTimeHP.ToString("F0");
            }
            else
            {
                timeHPText.gameObject.SetActive(false);
            }
        }

        if (currentTimeHP <= 0)
        {
            Debug.Log("Boss Die");
            //playerController.BackgroundBGM("Stage");
            bossPanel.SetActive(false);
            Destroy(gameObject);
        }

        if (isHit)
        {
            currentHitTime -= Time.deltaTime;
            if (currentHitTime <= 0)
            {
                isHit = false;
            }
        }
    }

    public void DecreaseBossHP(float damage)
    {
        if (!isHit)
        {
            currentTimeHP -= damage;
            isHit = true;
            currentHitTime = hitTime;
        }
    }

    public void StartBoss()
    {
        playerController = FindObjectOfType<PlayerController>();
        playerHealth = FindObjectOfType<PlayerHealth>();
        playerHealth.boss = this;
        playerHealth.bossStage = true;

        playerController.BackgroundBGM(bossName);

        playerHealth.bossStage = true;
        currentTimeHP = maxTimeHP;
        timeHPSlider.value = maxTimeHP;
        currentHitTime = 0;
    }

    public void ResetBoss()
    {
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        playerHealth.bossStage = false;
        playerController.BackgroundBGM("Stage");
        if (nextScene)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}