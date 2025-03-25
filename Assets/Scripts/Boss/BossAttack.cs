using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    public int damage;
    public bool hit;

    public void Attack(PlayerHealth player)
    {
        if (!hit)
        {
            player.TakeDamage(damage);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("bossAttack");
            var player = other.gameObject.GetComponentInChildren<PlayerHealth>();
            Attack(player);
            hit = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            hit = false;
        }
    }
}
