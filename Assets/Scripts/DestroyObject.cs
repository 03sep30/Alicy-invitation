using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    public GameObject activeObj;
    public GameObject hidePanel;

    public float destroyTime = 7f;

    public bool isStartDestroy = false;
    public bool isDestroy = false;
    public bool isDisable = false;
    public bool isIntroObj = false;

    private FadeController fadeController;

    void Start()
    {
        fadeController = FindAnyObjectByType<FadeController>();

        fadeController.OnFadeFinished += HandleFadeFinished;

        if (isStartDestroy || isIntroObj)
            StartDestroy(gameObject, destroyTime);
    }

    private void HandleFadeFinished()
    {
        if (isIntroObj)
        {
            
            Destroy(gameObject);
        }
    }

    public void StartDestroy(GameObject obj, float time)
    {
        StartCoroutine(DestroyObjectCoroutine(obj, time));
    }

    IEnumerator DestroyObjectCoroutine(GameObject obj, float time)
    {
        if (isIntroObj)
        {
            hidePanel.SetActive(false);
            yield return new WaitForSeconds(time);
            fadeController.StartFadeIn();
        }
        if (isDestroy)
        {
            yield return new WaitForSeconds(time);
            Destroy(gameObject);
        }
    }

    void OnDestroy()
    {
        if (activeObj != null)
            activeObj.SetActive(true);
        if (fadeController != null)
            fadeController.OnFadeFinished -= HandleFadeFinished;
    }
}