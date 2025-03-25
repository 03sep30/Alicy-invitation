using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BossHP : MonoBehaviour
{
    public float maxTimeHP;
    public float currentTimeHP;
    public bool isHit = false;
    public float hitTime;
    private float currentHitTime;
    public Slider timeHPSlider;
    public TextMeshProUGUI timeHPText;

    void Start()
    {
        currentTimeHP = maxTimeHP;
        timeHPSlider.value = maxTimeHP;
        currentHitTime = 0;
    }

    void Update()
    {
        if (currentTimeHP > 0)
        {
            currentTimeHP -= Time.deltaTime;
            timeHPSlider.value = currentTimeHP / maxTimeHP;
            if (currentTimeHP <= 3.1)
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
}