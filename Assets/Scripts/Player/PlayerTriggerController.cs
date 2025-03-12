using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTriggerController : MonoBehaviour
{
    public GameObject cheshire;
    public GameObject whiteScreen;
    public GameObject player;

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
            whiteScreen.SetActive(true);
            playerHealth.Die();
        }
        if (other.gameObject.CompareTag("SavePoint"))
        {
            playerHealth.SpawnPoint = other.gameObject.transform;
        }
    }
}
