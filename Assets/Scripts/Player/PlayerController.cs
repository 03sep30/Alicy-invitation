using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterSize
{
    Small,
    Normal,
    Big,
    None
}

public class PlayerController : MonoBehaviour
{
    public float maxPosition = 0;
    public CharacterSize currentSize;

    public GameObject LineObj;
    
    private PlayerMovement playerMovement;
    private PlayerHealth playerHealth;
    public CinemachineFreeLook CinemachineFreeLook;

    void Start()
    {
        currentSize = CharacterSize.Normal;
        playerMovement = GetComponent<PlayerMovement>();
        playerHealth = GetComponent<PlayerHealth>();
    }

    void Update()
    {
        DropCalculation();
    }

    void DropCalculation()
    {
        if (transform.position.y < LineObj.transform.position.y)
        {
            playerHealth.Die();
        }

        //if (!playerMovement.isJumping)
        //{
        //    if (maxPosition - transform.position.y > 10)
        //    {
        //        if (!playerHealth.isDrinkingTeacup)
        //        {
        //            playerHealth.Die();
        //        }
        //    }
        //    maxPosition = 0;
        //}
        //else
        //{
        //    if (playerMovement.rb.velocity.y < 0 && maxPosition < transform.position.y)
        //    {
        //        maxPosition = transform.position.y;
        //    }
        //}
    }

    private void UpdateCameraRig()
    {
        if (currentSize == CharacterSize.Big)
        {
            CinemachineFreeLook.m_YAxis.Value = 1f;
        }
        if(currentSize == CharacterSize.Small)
        {
            CinemachineFreeLook.m_YAxis.Value = 0f;
        }
        else
        {
            CinemachineFreeLook.m_YAxis.Value = 0.5f;
        }
    }    

    

    void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.CompareTag("Obstacle"))
        {
            playerMovement.isJumping = false;
        }
        if (coll.gameObject.CompareTag("Water"))
        {
            playerHealth.TakeDamage(1);
        }
        if (playerMovement.isBreaking && coll.gameObject.CompareTag("Breakable"))
        {
            coll.gameObject.GetComponent<ObstacleController>().Explosion();
            playerMovement.isBreaking = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            if (other.gameObject.CompareTag("ParryingObj"))
            {
                var parryingObj = other.gameObject.GetComponent<ParryingObj>();
                switch (parryingObj.Name)
                {
                    case "Juice":
                        playerHealth.TakeHealth(parryingObj.heal);
                        parryingObj.UpdateSize(gameObject);
                        UpdateCameraRig();
                        Destroy(other.transform.parent.gameObject);
                        break;
                    case "TeaCup":
                        parryingObj.Invincibility(gameObject);
                        StartCoroutine(TeaCupTime());
                        Destroy(other.transform.parent.gameObject);
                        break;
                    case "Raisin":
                        playerHealth.TakeHealth(parryingObj.heal);
                        parryingObj.UpdateSize(gameObject);
                        UpdateCameraRig();
                        Destroy(other.transform.parent.gameObject);
                        break;
                    case "Fan":
                        playerMovement.fanAvailable = true;
                        Destroy(other.transform.parent.gameObject);
                        break;
                    default:
                        break;
                }
            }
        }
    }

    IEnumerator TeaCupTime()
    {
        float originalJumpForce = playerMovement.jumpForce;
        playerMovement.jumpForce *= 2f;

        yield return new WaitForSeconds(10f);

        playerMovement.jumpForce = originalJumpForce;
        playerHealth.isDrinkingTeacup = false;
    }
}
