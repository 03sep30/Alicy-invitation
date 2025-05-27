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
    
    public float hitTime;
    private float currentHitTime;
    public Slider timeHPSlider;
    public TextMeshProUGUI timeHPText;
    public GameObject bossPanel;

    private PlayerController playerController;
    private PlayerHealth playerHealth;

    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        playerHealth = FindObjectOfType<PlayerHealth>();
        playerHealth.boss = this;

        playerController.BackgroundBGM(bossName);

        playerHealth.bossStage = true;
        currentTimeHP = maxTimeHP;
        timeHPSlider.value = maxTimeHP;
        currentHitTime = 0;
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
            playerController.BackgroundBGM("Stage");
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

    public void ResetBoss()
    {
        currentTimeHP = maxTimeHP;
    }

    private void OnDestroy()
    {
        playerHealth.bossStage = false;
        playerController.BackgroundBGM("Stage");
    }
}