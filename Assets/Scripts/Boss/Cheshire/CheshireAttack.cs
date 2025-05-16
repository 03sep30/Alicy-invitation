using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheshireAttack : BossAttack
{
    public Transform target;

    public bool isPreparingAttack = false;

    public override void Attack(PlayerHealth player)
    {
        base.Attack(player);
    }
}