using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public GameObject bossPanel;
    [Header("TimeHP")]
    public GameObject timePanel;
    public Slider timeHPSlider;
    public TextMeshProUGUI timeHPText;

    [Header("HeartHP")]
    public GameObject heartPanel;
    public Image[] playerHPImages;

    [Header("Mushroom")]
    public TextMeshProUGUI orangeMushroomText;
    public TextMeshProUGUI blueMushroomText;
    public Image orangeMushroomImage;
    public Image blueMushroomImage;
    public Sprite[] orangeMushroomSprite; // [0]: 선택됨, [1]: 비선택
    public Sprite[] blueMushroomSpirte;   // [0]: 선택됨, [1]: 비선택

    
    private PlayerHealth playerHealth;

    private void Start()
    {
        playerHealth = GetComponentInChildren<PlayerHealth>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            playerHealth.TakeDamage(1f);
        }
        if (playerHealth.currentHealthType == HealthType.Time)
        {
            if (!playerHealth.isDie)
            {
                bossPanel.SetActive(true);
                timePanel.SetActive(true);
                heartPanel.SetActive(false);
            }
            if (playerHealth.isDie)
            {
                bossPanel.SetActive(false);
            }

            PlayerTimeHPUI();
        }
        if (playerHealth.currentHealthType == HealthType.Heart)
        {
            bossPanel.SetActive(true);
            heartPanel.SetActive(true);
            timePanel.SetActive(false);
        }
        if (playerHealth.currentHealthType == HealthType.None)
        {
            bossPanel.SetActive(false);
        }
    }

    public void PlayerTimeHPUI()
    {
        if (!playerHealth.isDie)
        {
            if (playerHealth.currentTimeHP > 0)
            {
                playerHealth.currentTimeHP -= Time.deltaTime;
                timeHPSlider.value = playerHealth.currentTimeHP / playerHealth.maxTimeHP;
                if (playerHealth.currentTimeHP <= 3.1)
                {
                    timeHPText.gameObject.SetActive(true);
                    timeHPText.text = playerHealth.currentTimeHP.ToString("F1");
                }
                else
                {
                    timeHPText.gameObject.SetActive(false);
                }
            }
            if (playerHealth.currentTimeHP <= 0)
            {
                playerHealth.Die();
                playerHealth.currentTimeHP = playerHealth.maxTimeHP;
            }
        }
    }

    public void HealHPUI(float hp)
    {
        if (playerHealth.currentHealthType == HealthType.Heart)
        {
            for (int i = 0; i < (int)hp; i++)
            {
                playerHPImages[i].gameObject.SetActive(true);
            }
        }
        if (playerHealth.currentHealthType == HealthType.Time)
        {
            playerHealth.currentTimeHP += hp;
        }
    }

    public void TakeDamageUI(float hp)
    {
        if (playerHealth.currentHealthType == HealthType.Heart)
        {
            playerHPImages[(int)hp].gameObject.SetActive(false);
        }
        if (playerHealth.currentHealthType == HealthType.Time)
        {
            playerHealth.currentTimeHP -= hp;
        }
    }

    public void UpdateOrangeMushroomUI(int count, bool isSelected)
    {
        orangeMushroomText.text = $"X{count}";
        orangeMushroomImage.sprite = isSelected ? orangeMushroomSprite[0] : orangeMushroomSprite[1];
    }

    public void UpdateBlueMushroomUI(int count, bool isSelected)
    {
        blueMushroomText.text = $"X{count}";
        blueMushroomImage.sprite = isSelected ? blueMushroomSpirte[0] : blueMushroomSpirte[1];
    }
}
