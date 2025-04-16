using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerTriggerController : MonoBehaviour
{
    public GameObject cheshire;
    public GameObject GingerCookie;
    public GameObject stage2BossImage;
    public GameObject dustImage;
    public GameObject smokeImage;
    public float dustTime = 10f;
    public float dustImageTime = 3f;

    public MovingPlatform currentPlatform;
    private PlayerHealth playerHealth;
    private PlayerMovement playerMovement;
    private ThirdPersonController thirdPersonController;
    private PlayerController playerController;
    private BossAttack boss;
    public Rigidbody rb;

    void Start()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
        playerMovement = FindObjectOfType<PlayerMovement>();
        thirdPersonController = FindObjectOfType<ThirdPersonController>();
        playerController = FindObjectOfType<PlayerController>();
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.name == "Position1")
        {
            thirdPersonController.gameObject.transform.parent = collision.gameObject.transform;
        }
        
    }

    public void TriggerDust()
    {
        StartCoroutine(IDustTime(dustTime));
    }

    public IEnumerator IDustTime(float dustTime)
    {
        dustImage.SetActive(true);
        yield return new WaitForSeconds(dustImageTime);
        dustImage.SetActive(false);
        smokeImage.SetActive(true);
        yield return new WaitForSeconds(dustTime);
        smokeImage.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Trampoline"))
        {
            Trampoline trampoline = other.gameObject.GetComponent<Trampoline>();
            rb.AddForce(Vector3.up * trampoline.TrampolineForce, ForceMode.Impulse);
        }
        if (other.gameObject.CompareTag("ParryingObj"))
        {
            thirdPersonController.isParrying = true;
            playerHealth.PlayerHeal(10f);
        }
        if (other.gameObject.CompareTag("MovingPlatform"))
        {
            currentPlatform = other.gameObject.GetComponent<MovingPlatform>();
        }
        if (other.gameObject.name == "WTF")
        {
            playerController.enterPortal = true;
        }
        
        if (other.gameObject.CompareTag("Portal"))
        {
            var portal = other.GetComponent<Portal>();
            Transform targetPortal = portal.EnterPortal();

            if (targetPortal != null)
            {
                playerController.lastGroundedY = targetPortal.position.y;
                transform.position = targetPortal.position;
            }
        }

        if (other.CompareTag("Cheshire"))
        {
            Destroy(other.gameObject);
            cheshire.SetActive(true);
            Debug.Log("Cheshire");
        }

        if (other.CompareTag("Ginger"))
        {
            Destroy(other.gameObject);
            GingerCookie.SetActive(true);
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

        if (other.gameObject.CompareTag("BossAttack"))
        {
            Debug.Log("bossAttack");
            boss = other.gameObject.GetComponent<BossAttack>();
            if (boss != null)
            {
                boss.Attack(playerHealth);
                boss.hit = true;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("MovingPlatform"))
        {
            currentPlatform = null;
        }
        if (other.gameObject.CompareTag("SpeedGround") || other.gameObject.CompareTag("SlowdownGround"))
        {
            playerController.UpdateStatus(10);
            thirdPersonController.MoveSpeed = playerMovement.originalMoveSpeed;
            thirdPersonController.SprintSpeed = playerMovement.originalSprintSpeed;
        }
        if (other.gameObject.CompareTag("BossAttack"))
        {
            boss = other.gameObject.GetComponent<BossAttack>();
            if (boss != null)
            {
                boss.hit = false;
            }
            boss = null;
        }
    }
}
