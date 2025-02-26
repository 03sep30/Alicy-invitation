using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teacup : ParryingObj
{
    //public override void Invincibility(GameObject player)
    //{
    //    player.GetComponent<PlayerHealth>().isDrinkingTeacup = true;
    //    Debug.Log("Invincibility");
    //}

    public override void Jump(GameObject player)
    {
        player.GetComponent<PlayerMovement>().isJumping = false;
        Debug.Log("Jump");
    }
}