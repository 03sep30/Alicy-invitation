using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TTSController : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip[] ttsClips;

    private int currentIndex = 0;
    private bool isPlayingSequence = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
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
            isPlayingSequence = false;
        }
    }
}