using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    public int Damage;

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}
