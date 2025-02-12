using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    public GameObject ExplosionEffect;

    public void Explosion()
    {
        GameObject effect = Instantiate(ExplosionEffect, transform.position, Quaternion.identity);
        Destroy(effect, 2f);
        Destroy(gameObject);
    }
}
