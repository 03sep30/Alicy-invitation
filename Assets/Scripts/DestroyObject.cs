using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    public GameObject activeObj;

    public bool isDestroy = false;
    public bool isIntroObj = false;

    private FadeController fadeController;

    void Start()
    {
        fadeController = FindAnyObjectByType<FadeController>();

        fadeController.OnFadeFinished += HandleFadeFinished;

        if (isDestroy)
            StartDestroy(gameObject, 7f);
    }
    private void HandleFadeFinished()
    {
        if (isIntroObj)
        {
            if (isIntroObj && activeObj != null)
                activeObj.SetActive(true);
            Destroy(gameObject);
        }
    }

    public void StartDestroy(GameObject obj, float time)
    {
        StartCoroutine(DestroyObjectCoroutine(obj, time));
    }

    IEnumerator DestroyObjectCoroutine(GameObject obj, float time)
    {
        yield return new WaitForSeconds(time);
        fadeController.StartFadeIn();
    }

    void OnDestroy()
    {
        if (fadeController != null)
            fadeController.OnFadeFinished -= HandleFadeFinished;
    }
}