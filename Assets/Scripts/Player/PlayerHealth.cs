using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    private PlayerController playerController;

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

        fadeController.OnFadeFinished += HandleFadeFinished;

        SpawnPoint = startPoint;
        gameObject.transform.parent.position = SpawnPoint.transform.position;
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

        GameOverVFX.SetActive(true);
        StartCoroutine(GameOverTime());
    }

    private void HandleFadeFinished()
    {
        if (isDie)
        {
            GameOverVFX.SetActive(false);
            player.transform.position = SpawnPoint.position;

            // 플레이어가 스폰 포인트로 이동한 후, lastGroundedY를 업데이트
            // 만약 PlayerController에서 관리된다면 해당 스크립트에 접근하거나 이벤트로 처리합니다.
            FindObjectOfType<PlayerController>().lastGroundedY = SpawnPoint.position.y;
            player.transform.position = SpawnPoint.position;

            foreach (SkinnedMeshRenderer meshRenderer in meshRenderers)
            {
                meshRenderer.gameObject.SetActive(true);
            }
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
