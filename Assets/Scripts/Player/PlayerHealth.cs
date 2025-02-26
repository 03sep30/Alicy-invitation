using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    [Header("체력")]
    public bool isDrinkingTeacup = false;
    public bool isDie;

    private FadeController fadeController;
    public Transform SpawnPoint;

    [Header("데미지 이펙트")]
    public float damageEffectDuration = 1f;
    private MeshRenderer[] meshRenderers;
    private Color[] originalColors;
    

    void Start()
    {
        meshRenderers = GetComponentsInChildren<MeshRenderer>();
        fadeController = FindAnyObjectByType<FadeController>();

        originalColors = new Color[meshRenderers.Length];
        for (int i = 0; i < meshRenderers.Length; i++)
        {
            originalColors[i] = meshRenderers[i].material.color;
        }
        fadeController.OnFadeFinished += HandleFadeFinished;
    }

    void OnDestroy()
    {
        if (fadeController != null)
            fadeController.OnFadeFinished -= HandleFadeFinished;
    }


    public void Die()
    {
        Debug.Log("Die");
        isDie = true;
        fadeController.StartFadeIn();
    }

    private void HandleFadeFinished()
    {
        if (isDie)
        {
            transform.position = SpawnPoint.position;
        }
    }

    IEnumerator DamageEffect()
    {
        foreach(MeshRenderer meshRenderer in meshRenderers)
        {
            meshRenderer.material.color = Color.red;
        }

        yield return new WaitForSeconds(damageEffectDuration);

        for (int i = 0; i < meshRenderers.Length; i++)
        {
            meshRenderers[i].material.color = originalColors[i];
        }
    }    
}
