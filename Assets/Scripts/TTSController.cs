using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TTSController : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip[] ttsClips;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayTTS()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }
}