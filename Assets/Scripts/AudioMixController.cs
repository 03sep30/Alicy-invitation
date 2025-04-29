using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioMixController : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider BGMSlider;

    public void SetBgmVolme()
    {
        float value = Mathf.Clamp(BGMSlider.value, 0.0001f, 1f);
        audioMixer.SetFloat("BGM", Mathf.Log10(value) * 20);
    }
}
