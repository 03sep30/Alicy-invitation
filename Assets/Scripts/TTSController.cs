using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TTSController : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    public AudioClip[] ttsClips;

    public bool nextScene = false;
    public string sceneName;
    public int currentIndex = 0;
    public bool isPlayingSequence = false;

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
                LoadingManager.LoadScene(sceneName);
            }
            isPlayingSequence = false;
        }
    }
}