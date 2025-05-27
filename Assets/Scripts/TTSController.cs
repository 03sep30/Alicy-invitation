using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TTSController : MonoBehaviour
{
    private AudioSource audioSource;

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