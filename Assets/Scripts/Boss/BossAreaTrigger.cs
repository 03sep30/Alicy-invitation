using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAreaTrigger : MonoBehaviour
{
    [Header("0:GingerCookie, 1:Chef")]
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
            switch(bossName)
            {
                case "GingerCookie":
                    playerController.deathFallHeight = 1000f;
                    playerHealth.currentHealthType = HealthType.Heart;
                    playerHealth.maxHeartHP = 5;
                    //playerUI.UpdateHeartUI();
                    bossObj.transform.position = bossPosition.position;
                    bossObj.SetActive(true);
                    gameObject.SetActive(false);
                    break;

                case "Chef":
                    playerHealth.currentHealthType = HealthType.Heart;
                    playerHealth.maxHeartHP = 5;
                    //playerUI.UpdateHeartUI();
                    bossObj.transform.position = bossPosition.position;
                    bossObj.SetActive(true);
                    gameObject.SetActive(false);
                    break;  

                case "gdsj":
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
        }
    }
}
