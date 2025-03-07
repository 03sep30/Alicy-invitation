using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raisin : ParryingObj
{
    public override void UpdateSize(GameObject player)
    {
        var playerCtrl = player.GetComponent<PlayerController>();

        player.gameObject.transform.localScale = new Vector3(2.5f, 2.5f, 2.5f);
        playerCtrl.currentSize = CharacterSize.Small;
    }
}