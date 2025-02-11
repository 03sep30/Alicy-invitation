using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Juice : ParryingObj
{
    public override void UpdateSize(GameObject player)
    {
        player.gameObject.transform.localScale = new Vector3(10f, 10f, 10f);
        player.GetComponent<PlayerMovement>().canDash = true;
    }
}
