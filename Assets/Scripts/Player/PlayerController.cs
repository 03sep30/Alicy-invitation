using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterSize
{
    Small,
    Big,
    Normal,
    None
}

public class PlayerController : MonoBehaviour
{
    public float maxPosition = 0;
    public CharacterSize currentSize;

    public GameObject LineObj;
    
    private PlayerMovement playerMovement;
    private PlayerHealth playerHealth;

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
        if (transform.position.y < LineObj.transform.position.y && !playerHealth.isDie)
        {
            playerHealth.Die();
        }
    }

    void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.CompareTag("Obstacle"))
        {
            playerMovement.isJumping = false;
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
                parryingObj.Jump(gameObject);
                switch (parryingObj.Name)
                {
                    case "Juice":
                        parryingObj.UpdateSize(gameObject);
                        Destroy(other.transform.parent.gameObject);
                        break;
                    case "TeaCup":
                        //parryingObj.Invincibility(gameObject);
                        //StartCoroutine(TeaCupTime());
                        Destroy(other.transform.parent.gameObject);
                        break;
                    case "Raisin":
                        parryingObj.UpdateSize(gameObject);
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
