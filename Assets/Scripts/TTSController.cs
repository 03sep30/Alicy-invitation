using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TTSController : MonoBehaviour
{
    private AudioSource audioSource;
    public GameObject textPanel;
    public TextMeshProUGUI text;
    public Image image;
    public string[] textLines;
    public Sprite[] characterImages;

    public bool isTextEnable = false;
    private float currentTextTime;
    public float textInterval = 7f;
    private int textIndex = 0;

    private static TTSController activeTTS = null;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        currentTextTime = 0;
    }

    void Update()
    {
        if (isTextEnable && activeTTS == this)
        {
            currentTextTime -= Time.deltaTime;

            if (currentTextTime <= 0)
            {
                PlayText();
                currentTextTime = textInterval;
            }
        }
    }

    public void PlayTTS()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    public void PlayText()
    {
        if (textIndex < textLines.Length)
        {
            text.text = textLines[textIndex];
            image.sprite = characterImages[textIndex];
            textIndex++;
        }

        if (textIndex >= textLines.Length)
        {
            isTextEnable = false;

            if (activeTTS == this)
            {
                textPanel.SetActive(false);
                activeTTS = null;
            }

            textIndex = 0;
        }
    }

    public void StartTextDisplay()
    {
        if (activeTTS != null) return;

        activeTTS = this;
        textIndex = 0;
        isTextEnable = true;
        textPanel.SetActive(true);
    }
}
