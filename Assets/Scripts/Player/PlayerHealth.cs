using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    [Header("ü��")]
    public int maxHealth = 30;
    public int currentHealth;
    public bool isDrinkingTeacup = false;

    public TextMeshProUGUI healthText;

    [Header("������ ����Ʈ")]
    public float damageEffectDuration = 1f;
    private MeshRenderer[] meshRenderers;
    private Color[] originalColors;
    

    void Start()
    {
        currentHealth = maxHealth;
        HealthTextUpdate();

        meshRenderers = GetComponentsInChildren<MeshRenderer>();

        originalColors = new Color[meshRenderers.Length];
        for (int i = 0; i < meshRenderers.Length; i++)
        {
            originalColors[i] = meshRenderers[i].material.color;
        }
    }

    public void TakeDamage(int damage)
    {
        if (currentHealth > 0 && !isDrinkingTeacup)
        {
            currentHealth -= damage;
            HealthTextUpdate();
            StartCoroutine(DamageEffect());
        }
        else Die();
    }

    public void TakeHealth(int health)
    {
        if (currentHealth < maxHealth)
        {
            currentHealth += health;
        }
        HealthTextUpdate();
    }

    void HealthTextUpdate()
    {
        healthText.text = $"X{currentHealth.ToString()}";
    }

    public void Die()
    {
        Debug.Log("Die");
        currentHealth = 0;
        HealthTextUpdate();
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
