using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAreaTrigger : MonoBehaviour
{
    [Header("0:GingerCookie, 1:Chef, 2:HatMan")]
    public string bossName;
    public GameObject bossObj;
    public Transform bossPosition;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.gameObject.GetComponentInChildren<PlayerHealth>();
            PlayerUI playerUI = other.gameObject.GetComponent<PlayerUI>();
            PlayerController playerController = other.gameObject.GetComponent<PlayerController>();

            if (playerHealth.lastBossAreaTrigger == null)
            {
                playerHealth.lastBossAreaTrigger = gameObject;
            }
            switch(bossName)
            {
                case "GingerCookie":
                    if (bossObj.activeInHierarchy == false)
                    {
                        playerController.deathFallHeight = 1000f;
                        playerHealth.currentHealthType = HealthType.Heart;
                        playerHealth.maxHeartHP = 5;
                        //playerUI.UpdateHeartUI();
                        bossObj.SetActive(true);
                    }
                    else
                    {
                        playerHealth.bossStage = true;
                    }
                    break;

                case "Chef":
                    if (bossObj.activeInHierarchy == false)
                    {
                        playerHealth.currentHealthType = HealthType.Heart;
                        playerHealth.maxHeartHP = 5;
                        //playerUI.UpdateHeartUI();
                        bossObj.SetActive(true);
                    }
                    else
                    {
                        playerHealth.bossStage = true;
                    }
                    break;  

                case "HatMan":
                    if (bossObj.activeInHierarchy == false)
                    {
                        playerHealth.currentHealthType = HealthType.Heart;
                        playerHealth.maxHeartHP = 5;
                        //playerUI.UpdateHeartUI();
                        bossObj.SetActive(true);
                    }
                    else
                    {
                        playerHealth.bossStage = true;
                    }
                    
                    break;

                case "ji":
                    break;

                case "hfsdg":
                    break;

                case "hfdsg":
                    break;

                case "jfdgd":
                    break;
            }
            bossObj.transform.position = bossPosition.position;
            gameObject.SetActive(false);
        }
    }
}
