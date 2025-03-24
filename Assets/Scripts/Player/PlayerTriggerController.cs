using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerTriggerController : MonoBehaviour
{
    public GameObject cheshire;
    public GameObject stage2BossImage;

    private PlayerHealth playerHealth;
    private CharacterController characterController;

    void Start()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
        characterController = FindObjectOfType<CharacterController>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ani"))
        {
            Debug.Log("Ani");
            gameObject.transform.parent = collision.gameObject.transform;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ani"))
        {
            Debug.Log("Ani");
            gameObject.transform.parent = null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cheshire"))
        {
            Destroy(other.gameObject);
            cheshire.SetActive(true);
            Debug.Log("Cheshire");
        }
        if (other.name == "Stage2Trigger")
        {
            stage2BossImage.SetActive(true);
        }
        if (other.gameObject.CompareTag("SavePoint"))
        {
            playerHealth.SpawnPoint = other.gameObject.transform;
        }
    }
}
