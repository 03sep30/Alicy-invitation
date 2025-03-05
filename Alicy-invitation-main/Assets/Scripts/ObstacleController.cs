using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    public GameObject ExplosionEffect;
    public Transform ExplosionPosition;

    public void Explosion()
    {
        GameObject effect = Instantiate(ExplosionEffect, ExplosionPosition.transform.position, Quaternion.identity);
        Destroy(effect, 2f);
        Destroy(gameObject);
    }
}
