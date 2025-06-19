using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    private int nextSceneIndex;

    public bool cheshireActive = false;

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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            int currentScene = SceneManager.GetActiveScene().buildIndex;
            nextSceneIndex = currentScene + 1;
            string nextSceneName;
            
            switch (nextSceneIndex)
            {
                case 1:
                    nextSceneName = "TitleScene";
                    SceneManager.LoadScene(nextSceneName);
                    break;
                case 2:
                    nextSceneName = "CutScene1";
                    SceneManager.LoadScene(nextSceneName);
                    break;
                case 3:
                    nextSceneName = "Main_1";
                    SceneManager.LoadScene(nextSceneName);
                    break;
                case 4:
                    nextSceneName = "CutScene2";
                    SceneManager.LoadScene(nextSceneName);
                    break;
                case 5:
                    nextSceneName = "Main_2";
                    SceneManager.LoadScene(nextSceneName);
                    break;
                case 6:
                    nextSceneName = "Main_2BossHatMan_Map";
                    SceneManager.LoadScene(nextSceneName);
                    break;
                case 7:
                    nextSceneName = "Main_2BossCat_Map";
                    SceneManager.LoadScene(nextSceneName);
                    break;
                case 8:
                    nextSceneName = "CutScene3";
                    SceneManager.LoadScene(nextSceneName);
                    break;
            }
        }
    }
}
