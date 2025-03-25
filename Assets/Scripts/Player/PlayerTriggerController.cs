using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerTriggerController : MonoBehaviour
{
    public float TrampolineForce;

    public GameObject cheshire;
    public GameObject stage2BossImage;

    private PlayerHealth playerHealth;
    private CharacterController characterController;
    private PlayerMovement playerMovement;
    private ThirdPersonController thirdPersonController;

    void Start()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
        characterController = FindObjectOfType<CharacterController>();
        playerMovement = FindObjectOfType<PlayerMovement>();
        thirdPersonController = FindObjectOfType<ThirdPersonController>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        //if (collision.gameObject.name == "Position1")
        //{
        //    thirdPersonController.gameObject.transform.parent = collision.gameObject.transform;
        //}

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
        if (other.gameObject.CompareTag("Ani"))
        {
            Debug.Log("Ani");
            thirdPersonController.gameObject.transform.parent = other.gameObject.transform;
        }
        if (other.CompareTag("Trampoline"))
        {
           thirdPersonController._verticalVelocity = Mathf.Sqrt(TrampolineForce * -2f * thirdPersonController.Gravity);
        }    
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
            float newMoveSpeed = playerMovement.originalMoveSpeed * 2;
            float newSprintSpeed = playerMovement.originalSprintSpeed * 2;
            thirdPersonController.MoveSpeed = newMoveSpeed;
            thirdPersonController.MoveSpeed = newSprintSpeed;
            Debug.Log("SpeedGround");
        }
        if (other.gameObject.CompareTag("SlowdownGround"))
        {
            float newMoveSpeed = playerMovement.originalMoveSpeed / 2;
            thirdPersonController.MoveSpeed = newMoveSpeed;
            Debug.Log("SlowdownGround");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("SpeedGround") || other.gameObject.CompareTag("SlowdownGround"))
        {
            thirdPersonController.MoveSpeed = playerMovement.originalMoveSpeed;
            thirdPersonController.SprintSpeed = playerMovement.originalSprintSpeed;
        }
        if (other.gameObject.CompareTag("Ani"))
        {
            Debug.Log("Ani");
            thirdPersonController.gameObject.transform.parent = null;
        }
    }
}
