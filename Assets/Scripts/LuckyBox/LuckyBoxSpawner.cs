using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuckyBoxSpawner : MonoBehaviour
{
    public GameObject luckyBoxPrefab;

    public int luckyBoxCount = 2;
    public float randomMinOffsetX;
    public float randomMaxOffsetX;
    public float randomOffsetY;
    public float randomMinOffsetZ;
    public float randomMaxOffsetZ;
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
            for (int i = 0; i < luckyBoxCount; i++)
            {
                Vector3 randomOffset = new Vector3(
                    Random.Range(randomMinOffsetX, randomMaxOffsetX),
                    randomOffsetY,
                    Random.Range(randomMinOffsetZ, randomMaxOffsetZ)
                );

                Vector3 spawnPos = platform.position + randomOffset;

                Instantiate(luckyBoxPrefab, spawnPos, Quaternion.identity);
            }
        }
    }

}
