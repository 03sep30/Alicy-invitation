using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSpawner : MonoBehaviour
{
    public GameObject WaterPrefab;
    public float spawnInterval = 2f;

    private float timer = 0f;

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            SpawnWater();
            timer = spawnInterval;
        }
    }

    void SpawnWater()
    {
        float randomX = Random.Range(-20f, 20f);
        Vector3 spawnPosition = new Vector3(randomX, 80f, randomX);
        Instantiate(WaterPrefab, spawnPosition, Quaternion.identity);
    }
}
