using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lollipop : MonoBehaviour
{
    private GingerCookieAttack gingerCookie;

    private void Start()
    {
        gingerCookie = FindObjectOfType<GingerCookieAttack>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.gameObject.GetComponentInChildren<PlayerHealth>();
            if (playerHealth != null && gingerCookie != null)
            {
                playerHealth.TakeDamage(gingerCookie.lollipopDamage);
            }
        }
        if (other.gameObject.layer == 7 || other.gameObject.layer == 0)
            Destroy(gameObject);
    }
}