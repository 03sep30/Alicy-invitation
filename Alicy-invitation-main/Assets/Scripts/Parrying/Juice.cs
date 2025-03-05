using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Juice : ParryingObj
{
    public override void UpdateSize(GameObject player)
    {
        var playerCtrl = player.GetComponent<PlayerController>();

        player.gameObject.transform.localScale = new Vector3(10f, 10f, 10f);
        playerCtrl.currentSize = CharacterSize.Big;
    }
}