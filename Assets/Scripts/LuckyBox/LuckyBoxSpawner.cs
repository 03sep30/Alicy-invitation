using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuckyBoxSpawner : MonoBehaviour
{
    public GameObject luckyBoxPrefab;
    public float spawnInterval = 20f;
    public Transform[] platforms;

    void Start()
    {
        InvokeRepeating(nameof(SpawnLuckyBoxesOnAllPlatforms), 5f, spawnInterval);
    }
    void SpawnLuckyBoxesOnAllPlatforms()
    {
        foreach (Transform platform in platforms)
        {
            for (int i = 0; i < 2; i++)
            {
                Vector3 randomOffset = new Vector3(
                    Random.Range(-3.5f, 3.5f),
                    0f,
                    Random.Range(-1f, 1f)
                );

                float yOffset = 0.5f;
                Vector3 spawnPos = platform.position + randomOffset + Vector3.up * yOffset;

                Instantiate(luckyBoxPrefab, spawnPos, Quaternion.identity);
            }
        }
    }

}
