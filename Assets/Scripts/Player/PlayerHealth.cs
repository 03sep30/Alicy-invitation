using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum HealthType
{
    Time,
    Heart,
    None
}

public class PlayerHealth : MonoBehaviour
{
    private PlayerController playerController;
    private PlayerUI playerUI;

    [Header("Boss")]

    public float maxTimeHP = 150f;
    public float currentTimeHP;
    public bool shield = false;
    public GameObject lastBossAreaTrigger;

    public BossHP boss;
    
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

    public GameObject hidePanel;
    public GameObject bossPanel;

    public bool bossStage = false;

    void Start()
    {
        GameManager.Instance.currentHealthType = HealthType.Heart;
        GameManager.Instance.maxHeartHP = 5;

        meshRenderers = GetComponentsInChildren<SkinnedMeshRenderer>();
        fadeController = FindAnyObjectByType<FadeController>();
        playerController = FindObjectOfType<PlayerController>();
        playerUI = FindObjectOfType<PlayerUI>();

        fadeController.OnFadeFinished += HandleFadeFinished;
        GameManager.Instance.currentHeartHP = GameManager.Instance.maxHeartHP;
        currentTimeHP = maxTimeHP;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
            PlayerHeal(1);
    }

    public void PlayerHeal(float heal)
    {
        if (GameManager.Instance.currentHealthType == HealthType.Heart)
        {
            if (GameManager.Instance.currentHeartHP < GameManager.Instance.maxHeartHP)
            {
                if (GameManager.Instance.currentHeartHP + heal > GameManager.Instance.maxHeartHP)
                {
                    GameManager.Instance.currentHeartHP = GameManager.Instance.maxHeartHP;
                    playerUI.HealHPUI(GameManager.Instance.currentHeartHP);
                }
                else
                {
                    GameManager.Instance.currentHeartHP += (int)heal;
                    playerUI.HealHPUI(GameManager.Instance.currentHeartHP);
                }
            }
        }
        if (GameManager.Instance.currentHealthType == HealthType.Time)
        {
            if (currentTimeHP < maxTimeHP)
            {
                currentTimeHP += heal;

            }
        }
    }

    public void TakeDamage(float damage)
    {
        if (GameManager.Instance.currentHealthType == HealthType.Heart)
        {
            if (GameManager.Instance.currentHeartHP > 0 && !shield)
            {
                GameManager.Instance.currentHeartHP -= (int)damage;
                playerUI.TakeDamageUI(GameManager.Instance.currentHeartHP);
            }
            if (shield)
            {
                shield = false;
            }
        }
        if (GameManager.Instance.currentHealthType == HealthType.Time)
        {
            if (currentTimeHP > 0)
            {
                currentTimeHP -= damage;
            }
        }
        if (GameManager.Instance.currentHeartHP <= 0 || currentTimeHP <= 0)
        {
            playerUI.TakeDamageUI(GameManager.Instance.currentHeartHP);
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

        if (bossStage && boss != null)
        {
            boss.isPaused = true;
            bossStage = false;
            boss.ResetBoss();
            if (lastBossAreaTrigger != null && lastBossAreaTrigger.activeInHierarchy == false)
                lastBossAreaTrigger.SetActive(true);
        }

        if (GameManager.Instance.currentHealthType == HealthType.Heart)
        {
            if (GameManager.Instance.currentHeartHP > 0)
            {
                TakeDamage(GameManager.Instance.currentHeartHP);
            }
        }
        if (GameManager.Instance.currentHealthType == HealthType.Time)
        {
            if (currentTimeHP > 0)
            {
                TakeDamage((int)currentTimeHP);
            }
        }
        hidePanel.SetActive(false);

        GameManager.Instance.currentSize = CharacterSize.Normal;
        playerController.UpdateStatus(10);
        if (playerController.currentEffect != null)
        {
            playerController.currentEffect.RemoveEffect();
            playerController.currentEffect = null;
        }
        if (playerController.bossPanel != null)
            playerController.bossPanel.SetActive(false);

        GameOverVFX.SetActive(true);
        StartCoroutine(GameOverTime());
    }

    private void HandleFadeFinished()
    {
        if (isDie)
        {
            if (boss != null)
                boss.isPaused = false;

            PlayerHeal(GameManager.Instance.maxHeartHP);
            PlayerHeal(maxTimeHP);
            playerController.BackgroundBGM("Stage");
            GameOverVFX.SetActive(false);
            player.transform.position = SpawnPoint.position;
            
            playerController.lastGroundedY = SpawnPoint.position.y;

            foreach (SkinnedMeshRenderer meshRenderer in meshRenderers)
            {
                meshRenderer.gameObject.SetActive(true);
            }
            playerController.crushing = false;
            
            hidePanel.SetActive(true);
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
                playerController.ttsAudioSource.clip = GameOver0;
                playerController.ttsAudioSource.Play();
                break;
            case 1:
                playerController.ttsAudioSource.clip = GameOver1;
                playerController.ttsAudioSource.Play();
                break;
            case 2:
                playerController.ttsAudioSource.clip = GameOver2;
                playerController.ttsAudioSource.Play();
                break;
        }
    }
}
