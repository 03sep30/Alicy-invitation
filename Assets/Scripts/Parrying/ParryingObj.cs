using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ParryingObj : MonoBehaviour
{
    public string Name;
    public int heal;

    public virtual void UpdateSize(GameObject player)
    {
        player.gameObject.transform.localScale = new Vector3(5f, 5f, 5f);
    }

    public virtual void Invincibility(GameObject player)
    {
        player.GetComponentInChildren<PlayerHealth>().isDrinkingTeacup = true;
        Debug.Log("Invincibility");
    }

    public virtual void Jump(GameObject player)
    {
        player.GetComponent<PlayerMovement>().isJumping = false;
        Debug.Log("Jump");
    }
}
