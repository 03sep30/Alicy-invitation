using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerTriggerController : MonoBehaviour
{
    public GameObject cheshire;

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

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            characterController.Move(Vector3.down * 0.1f);
            Debug.Log("GroundTrigger");
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
