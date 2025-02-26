using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void FadeEventHandler();
public class FadeController : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    public float fadeTime = 1f;
    public float fadeInTime = 0.2f;
    public float fadeOutTime = 3f;
    float accumTime = 0f;
    private Coroutine fadeCor;
    public bool fadeFinished = false;

    public event FadeEventHandler OnFadeFinished;
    
    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void StartFadeIn()
    {
        if (fadeCor != null)
        {
            StopAllCoroutines();
            fadeCor = null;
        }
        fadeCor = StartCoroutine(FadeIn());
    }    

    private IEnumerator FadeIn()
    {
        yield return new WaitForSeconds(fadeInTime);
        accumTime = 0f;
        while (accumTime < fadeTime)
        {
            canvasGroup.alpha = Mathf.Lerp(0f, 1f, accumTime / fadeTime);
            yield return 0;
            accumTime += Time.deltaTime;
        }
        canvasGroup.alpha = 1f;

        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(fadeOutTime);
        accumTime = 0f;
        OnFadeFinished?.Invoke();
        while (accumTime < fadeTime)
        {
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, accumTime / fadeTime);
            yield return 0;
            accumTime += Time.deltaTime;
        }
        canvasGroup.alpha = 0f;
        fadeFinished = true;
    }
}
