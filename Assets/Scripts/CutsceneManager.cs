using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem.LowLevel;

public class CutsceneManager : MonoBehaviour
{
    public Image CutsceneImage;
    public Sprite[] cutsceneImages;
    public float imageTime = 2f;
    public bool nextScene = false;
    public string sceneName;

    private int currentIndex = 0;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        if (cutsceneImages.Length > 0 && CutsceneImage != null)
        {
            StartCoroutine(PlayCutscene());
        }
    }

    IEnumerator PlayCutscene()
    {
        while (currentIndex < cutsceneImages.Length)
        {
            CutsceneImage.sprite = cutsceneImages[currentIndex];
            currentIndex++;
            yield return new WaitForSeconds(imageTime);
        }

        if (nextScene)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
