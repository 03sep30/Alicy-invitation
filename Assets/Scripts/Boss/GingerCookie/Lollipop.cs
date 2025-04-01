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
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(gingerCookie.damage);
                Destroy(gameObject);
            }
        }
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }
}
