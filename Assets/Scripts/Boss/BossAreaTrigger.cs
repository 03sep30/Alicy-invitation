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
            if (playerHealth == null) return;
            if (playerHealth.isRespawning) return;
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
                        GameManager.Instance.currentHealthType = HealthType.Heart;
                        GameManager.Instance.maxHeartHP = 5;
                        //playerUI.UpdateHeartUI();
                        BossHP bossHP = bossObj.GetComponent<BossHP>();
                        playerController.bossHP = bossHP;
                        playerHealth.boss = bossHP;
                        bossObj.SetActive(true);
                        bossHP.StartBoss();
                    }
                    else
                    {
                        playerHealth.bossStage = true;
                    }
                    break;

                case "Chef":
                    if (bossObj.activeInHierarchy == false)
                    {
                        GameManager.Instance.currentHealthType = HealthType.Heart;
                        GameManager.Instance.maxHeartHP = 5;
                        //playerUI.UpdateHeartUI();
                        BossHP bossHP = bossObj.GetComponent<BossHP>();
                        playerController.bossHP = bossHP;
                        playerHealth.boss = bossHP;
                        bossObj.SetActive(true);
                        bossHP.StartBoss();
                    }
                    else
                    {
                        playerHealth.bossStage = true;
                    }
                    break;  

                case "HatMan":
                    if (bossObj.activeInHierarchy == false)
                    {
                        GameManager.Instance.currentHealthType = HealthType.Heart;
                        GameManager.Instance.maxHeartHP = 5;
                        //playerUI.UpdateHeartUI();
                        BossHP bossHP = bossObj.GetComponent<BossHP>();
                        playerController.bossHP = bossHP;
                        playerHealth.boss = bossHP;
                        bossObj.SetActive(true);
                        bossHP.StartBoss();
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
