using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrangeMushroom : Mushroom
{
    public override void ChangeState(PlayerController player)
    {
        player.orangeMushroomCount++;
    }
}
