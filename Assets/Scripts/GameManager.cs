using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int orangeMushroomCount = 0;
    public int blueMushroomCount = 0;
    public int greenMushroomCount = 0;
    public MushroomType currentMushroom;

    public CharacterSize currentSize;

    public int maxHeartHP = 3;
    public int currentHeartHP;
    public HealthType currentHealthType;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
}
