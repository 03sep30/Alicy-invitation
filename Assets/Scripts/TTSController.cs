using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TTSController : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip[] ttsClips;

    public bool nextScene = false;
    public string sceneName;
    public int currentIndex = 0;
    public bool isPlayingSequence = false;

    private PlayerController playerController;

    private void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        audioSource = playerController.audioSource;
    }

    void Update()
    {
        if (isPlayingSequence && !audioSource.isPlaying)
        {
            PlayNextClip();
        }
    }

    public void PlayTTS()
    {
        if (ttsClips.Length == 0) return;

        currentIndex = 0;
        isPlayingSequence = true;
        PlayNextClip();
    }

    private void PlayNextClip()
    {
        if (currentIndex < ttsClips.Length)
        {
            audioSource.clip = ttsClips[currentIndex];
            audioSource.Play();
            currentIndex++;
        }
        else
        {
            if (nextScene)
            {
                SceneManager.LoadScene(sceneName);
            }
            isPlayingSequence = false;
        }
    }
}