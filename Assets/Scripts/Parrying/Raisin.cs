using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raisin : ParryingObj
{
    public override void UpdateSize(GameObject player)
    {
        player.gameObject.transform.localScale = new Vector3(2.5f, 2.5f, 2.5f);
        GameManager.Instance.currentSize = CharacterSize.Small;
    }
}