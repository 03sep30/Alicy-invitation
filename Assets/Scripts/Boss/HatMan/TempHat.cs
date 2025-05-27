using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempHat : MonoBehaviour
{
    public GameObject hat;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponentInChildren<PlayerHealth>();

            if (playerHealth != null)
            {
                playerHealth.TakeDamage(1);
                StartCoroutine(HatDestroy());   
            }
        }
    }

    private IEnumerator HatDestroy()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(hat);
    }
}
