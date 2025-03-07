using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JuiceSpawner : MonoBehaviour
{
    public GameObject JuicePrefab;

    public float spawnInterval = 6f;
    private float timer = 0f;

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0 )
        {
            SpawnJuice();
            timer = spawnInterval;
        }
    }

    void SpawnJuice()
    {
        float randomX = Random.Range(-20f, 20f);
        Vector3 spawnPosition = new Vector3(randomX, 80f, randomX);
        Instantiate(JuicePrefab, spawnPosition, Quaternion.identity);
    }    
}
