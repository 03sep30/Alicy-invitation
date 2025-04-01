using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    public int damage;
    public bool hit;

    public virtual void Attack(PlayerHealth player)
    {
        if (!hit && player != null)
        {
            player.TakeDamage(damage);
        }
    }
}
