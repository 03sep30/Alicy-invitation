using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float maxPosition = 0;
    
    private PlayerMovement playerMovement;
    private PlayerHealth playerHealth;
    public CinemachineFreeLook CinemachineFreeLook;

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerHealth = GetComponent<PlayerHealth>();
    }

    void Update()
    {
        DropCalculation();
    }

    void DropCalculation()
    {
        if (!playerMovement.isJumping)
        {
            if (maxPosition - transform.position.y > 10)
            {
                if (!playerHealth.isDrinkingTeacup)
                {
                    playerHealth.TakeDamage(5);
                }
            }
            maxPosition = 0;
        }
        else
        {
            if (playerMovement.rb.velocity.y < 0 && maxPosition < transform.position.y)
            {
                maxPosition = transform.position.y;
            }
        }
    }

    private void UpdateCameraRig()
    {
        float playerHeight = gameObject.transform.localScale.y;
        if (playerHeight >= 10f)
        {
            CinemachineFreeLook.m_YAxis.Value = 1f;
        }
        else if(playerHeight <= 2.5f)
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
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("ParryingObj"))
        {
            var parryingObj = other.gameObject.GetComponent<ParryingObj>();

            switch (parryingObj.ID)
            {
                case 0:
                    playerHealth.TakeHealth(parryingObj.heal);
                    parryingObj.UpdateSize(gameObject);
                    UpdateCameraRig();
                    Destroy(other.transform.parent.gameObject);
                    break;
                case 1:
                    parryingObj.Invincibility(gameObject);
                    Destroy(other.transform.parent.gameObject);
                    break;
                default:
                    break;
            }
        }

        if (other.gameObject.CompareTag("Water"))
        {
            playerHealth.TakeDamage(1);
        }
    }
}
