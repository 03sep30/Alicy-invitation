using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerTriggerController : MonoBehaviour
{
    public GameObject cheshire;
    public GameObject stage2BossImage;

    private PlayerHealth playerHealth;
    private CharacterController characterController;
    private PlayerMovement playerMovement;
    private ThirdPersonController thirdPersonController;
    private PlayerController playerController;

    void Start()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
        characterController = FindObjectOfType<CharacterController>();
        playerMovement = FindObjectOfType<PlayerMovement>();
        thirdPersonController = FindObjectOfType<ThirdPersonController>();
        playerController = FindObjectOfType<PlayerController>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        //if (collision.gameObject.name == "Position1")
        //{
        //    thirdPersonController.gameObject.transform.parent = collision.gameObject.transform;
        //}

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
        if (other.gameObject.CompareTag("SpeedGround"))
        {
            playerController.UpdateStatus(2);
            float newMoveSpeed = playerMovement.originalMoveSpeed * 2;
            float newSprintSpeed = playerMovement.originalSprintSpeed * 2;
            thirdPersonController.MoveSpeed = newMoveSpeed;
            thirdPersonController.SprintSpeed = newSprintSpeed;
            Debug.Log("SpeedGround");
        }
        if (other.gameObject.CompareTag("SlowdownGround"))
        {
            playerController.UpdateStatus(3);
            float newMoveSpeed = playerMovement.originalMoveSpeed / 2;
            float newSprintSpeed = playerMovement.originalSprintSpeed / 2;
            thirdPersonController.MoveSpeed = newMoveSpeed;
            thirdPersonController.SprintSpeed = newSprintSpeed;
            Debug.Log("SlowdownGround");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("SpeedGround") || other.gameObject.CompareTag("SlowdownGround"))
        {
            playerController.UpdateStatus(10);
            thirdPersonController.MoveSpeed = playerMovement.originalMoveSpeed;
            thirdPersonController.SprintSpeed = playerMovement.originalSprintSpeed;
        }
    }
}
