using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingManager : MonoBehaviour
{
    public static string nextScene;

    [SerializeField] private Slider Progress;
    [SerializeField] private Image LoadingPanelImage;

    private float time;
    private float loadingTime;

    private void Start()
    {
        loadingTime = Random.Range(1f, 3f);
        Debug.Log(loadingTime);

        StartCoroutine(LoadSceneCoroutine());
    }

    public static void LoadScene(string sceneName)
    {
        nextScene = sceneName;
        SceneManager.LoadScene("LoadingScene");
    }

    IEnumerator LoadSceneCoroutine()
    {
        //yield return null;

        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene); 
        op.allowSceneActivation = false;    // ���� �ε��� ������ �ڵ����� �ҷ��� ������ �̵��� ���ΰ�  false -> ���� ������ ��ȯ���� �ʰ� ���

        float time = 0f;

        while (!op.isDone)
        {
            time += Time.deltaTime;
            Progress.value = Mathf.Clamp01(time / loadingTime);

            if (time > loadingTime)
            {
                op.allowSceneActivation = true;
            }
            yield return null;

            //float ProgressValue = Mathf.Clamp01(op.progress / 0.9f);
            //Progress.value = ProgressValue;

            //if (op.progress >= 0.9f)
            //{
            //    Progress.value = 1.0f;
            //    op.allowSceneActivation = true;
            //    yield break;
            //}
        }
    }
}
