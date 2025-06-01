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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.gameObject.GetComponentInChildren<PlayerHealth>();
            if (playerHealth != null && gingerCookie != null)
            {
                playerHealth.TakeDamage(gingerCookie.lollipopDamage);
            }
            Debug.Log("¥Í¿Ω");
        }
        Destroy(gameObject);
    }
}