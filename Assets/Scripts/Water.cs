using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    public int Damage;

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
