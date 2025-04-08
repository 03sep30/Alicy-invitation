using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueMushroom : Mushroom
{
    public override void ChangeState(PlayerController player)
    {
        player.blueMushroomCount++;
    }
}
