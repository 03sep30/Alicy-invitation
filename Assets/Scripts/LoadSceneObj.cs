using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneObj : MonoBehaviour
{
    public string sceneName;

    public void LoadScene()
    {
        LoadingManager.LoadScene(sceneName);
    }
}
