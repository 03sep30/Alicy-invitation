using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextController : MonoBehaviour
{
    public GameObject textPanel;
    public TextMeshProUGUI text;
    public Image image;
    public string[] textLines;
    public Sprite[] characterImages;
    public float[] textIntervals;

    public bool isTextEnable = false;
    private float currentTextTime;
    private int textIndex = 0;

    private static TextController activeText = null;

    void Start()
    {
        currentTextTime = 0;

        if (textIntervals.Length != textLines.Length)
        {
            float defaultInterval = 2f;
            float[] newIntervals = new float[textLines.Length];

            for (int i = 0; i < newIntervals.Length; i++)
            {
                if (i < textIntervals.Length)
                    newIntervals[i] = textIntervals[i];
                else
                    newIntervals[i] = defaultInterval;
            }

            textIntervals = newIntervals;
        }
    }

    void Update()
    {
        if (textLines.Length > 0 && characterImages.Length > 0)
        {
            if (isTextEnable && activeText == this)
            {
                currentTextTime -= Time.deltaTime;

                if (currentTextTime <= 0)
                {
                    PlayText();

                    if (textIndex < textIntervals.Length)
                        currentTextTime = textIntervals[textIndex];
                    else
                        currentTextTime = 1f;
                }
            }
        }
    }

    public void PlayText()
    {
        if (textIndex < textLines.Length)
        {
            text.text = textLines[textIndex];
            image.sprite = characterImages[textIndex];
            textIndex++;
            Debug.Log($"currentIndex : {textIndex}");
        }
        else
        {
            isTextEnable = false;

            if (activeText == this)
            {
                textPanel.SetActive(false);
                activeText = null;
            }

            textIndex = 0;
        }
    }

    public void StartTextDisplay()
    {
        if (activeText != null) return;

        activeText = this;
        textIndex = 0;
        isTextEnable = true;
        textPanel.SetActive(true);

        if (textLines.Length > 0)
        {
            text.text = textLines[0];
            image.sprite = characterImages[0];
            textIndex = 1;

            if (textIntervals.Length > 0)
                currentTextTime = textIntervals[0];
            else
                currentTextTime = 3f;
        }
    }
}
