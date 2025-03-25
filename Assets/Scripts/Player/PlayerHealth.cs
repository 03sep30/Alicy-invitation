using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    private PlayerController playerController;
    private CharacterController characterController;
    private PlayerUI playerUI;

    [Header("Boss")]
    public int maxPlayerHP = 3;
    public int currentPlayerHP;
    public bool shield = false;

    [Header("????")]
    public bool isDrinkingTeacup = false;
    public bool isDie;

    private FadeController fadeController;
    public Transform SpawnPoint;
    public Transform startPoint;
    public GameObject player;

    [Header("?????? ??????")]
    public GameObject GameOverVFX;
    public float GameOverVFXDuration = 2f;
    private SkinnedMeshRenderer[] meshRenderers;

    [Header("TTS")]
    public AudioClip GameOver0;
    public AudioClip GameOver1;
    public AudioClip GameOver2;

    void Start()
    {
        meshRenderers = GetComponentsInChildren<SkinnedMeshRenderer>();
        fadeController = FindAnyObjectByType<FadeController>();
        playerController = FindObjectOfType<PlayerController>();
        characterController = FindObjectOfType<CharacterController>();
        playerUI = FindObjectOfType<PlayerUI>();

        fadeController.OnFadeFinished += HandleFadeFinished;

        SpawnPoint = startPoint;
        gameObject.transform.parent.position = SpawnPoint.transform.position;
        currentPlayerHP = maxPlayerHP;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            TakeDamage(1);
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            PlayerHeal(1);
        }
    }

    public void PlayerHeal(int heal)
    {
        if (currentPlayerHP < maxPlayerHP)
        {
            currentPlayerHP += heal;
            playerUI.HealHPUI(currentPlayerHP);
        }
    }

    public void TakeDamage(int damage)
    {
        if (currentPlayerHP > 0 && !shield)
        {
            currentPlayerHP -= damage;
            playerUI.TakeDamageUI(currentPlayerHP);
        }
        if (shield)
        {
            shield = false;
        }
        if (currentPlayerHP <= 0)
        {
            playerUI.TakeDamageUI(currentPlayerHP);
            Die();
        }
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
        playerController.currentSize = CharacterSize.Normal;
        if (playerController.currentEffect != null)
        {
            playerController.currentEffect.RemoveEffect();
            playerController.currentEffect = null;
        }

        GameOverVFX.SetActive(true);
        StartCoroutine(GameOverTime());
    }

    private void HandleFadeFinished()
    {
        if (isDie)
        {
            PlayerHeal(maxPlayerHP);
            GameOverVFX.SetActive(false);
            characterController.enabled = false;
            player.transform.position = SpawnPoint.position;
            characterController.enabled = true;
            
            playerController.lastGroundedY = SpawnPoint.position.y;

            foreach (SkinnedMeshRenderer meshRenderer in meshRenderers)
            {
                meshRenderer.gameObject.SetActive(true);
            }
            playerController.crushing = false;
            isDie = false;  
        }
    }

    IEnumerator GameOverTime()
    {
        yield return new WaitForSeconds(GameOverVFXDuration);

        foreach (SkinnedMeshRenderer meshRenderer in meshRenderers)
        {
            meshRenderer.gameObject.SetActive(false);
        }

        fadeController.StartFadeIn();
    }

    public void RandomGameOverTTS()
    {
        int random = Random.Range(0, 3);
        switch (random)
        {
            case 0:
                playerController.audioSource.clip = GameOver0;
                playerController.audioSource.Play();
                break;
            case 1:
                playerController.audioSource.clip = GameOver1;
                playerController.audioSource.Play();
                break;
            case 2:
                playerController.audioSource.clip = GameOver2;
                playerController.audioSource.Play();
                break;
        }
    }
}
