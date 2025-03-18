using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerTriggerController : MonoBehaviour
{
    public GameObject cheshire;

    private PlayerHealth playerHealth;

    void Start()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cheshire"))
        {
            cheshire.SetActive(true);
            Debug.Log("Cheshire");
        }
        if (other.CompareTag("OvenDoor"))
        {
            //스테이지 2 프로토타입 씬 이동
        }
        if (other.gameObject.CompareTag("SavePoint"))
        {
            playerHealth.SpawnPoint = other.gameObject.transform;
        }
    }
}
