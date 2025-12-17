using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempHat : MonoBehaviour
{
    public GameObject hat;

    void Start()
    {
        StartCoroutine(HatDestroy(5f));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponentInChildren<PlayerHealth>();

            if (playerHealth != null)
            {
                playerHealth.TakeDamage(1);
                StartCoroutine(HatDestroy(1.5f));   
            }
        }
    }

    private IEnumerator HatDestroy(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(hat);
    }
}
