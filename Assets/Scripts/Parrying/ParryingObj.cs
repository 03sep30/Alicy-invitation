using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ParryingObj : MonoBehaviour
{
    public int ID;
    public int heal;

    public virtual void UpdateSize(GameObject player)
    {
        player.gameObject.transform.localScale = new Vector3(5f, 5f, 5f);
    }

    public virtual void Invincibility(GameObject player)
    {
        player.GetComponent<PlayerHealth>().isDrinkingTeacup = true;
    }
}
