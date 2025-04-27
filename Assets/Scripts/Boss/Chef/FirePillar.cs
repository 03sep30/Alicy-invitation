using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePillar : MonoBehaviour
{
    [SerializeField] private float firePillarTime;
    [SerializeField] private int damage;

    void Start()
    {
        StartCoroutine(FirePillarTime());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.gameObject.GetComponentInChildren<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
        }
    }

    private IEnumerator FirePillarTime()
    {
        yield return new WaitForSeconds(firePillarTime);
        Destroy(gameObject);
    }
}
