using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teacup : ParryingObj
{
    public override void Invincibility(GameObject player)
    {
        base.Invincibility(player);
        player.GetComponent<PlayerHealth>().shield = 1;
    }

}
