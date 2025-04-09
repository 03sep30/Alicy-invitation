using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public Image[] playerHPImages;
    public TextMeshProUGUI orangeMushroomText;
    public TextMeshProUGUI blueMushroomText;
    public Image orangeMushroomImage;
    public Image blueMushroomImage;
    public Sprite[] orangeMushroomSprite; // [0]: 선택됨, [1]: 비선택
    public Sprite[] blueMushroomSpirte;   // [0]: 선택됨, [1]: 비선택

    public void HealHPUI(int hp)
    {
        for (int i = 0; i < hp; i++)
        {
            playerHPImages[i].gameObject.SetActive(true);
        }
    }

    public void TakeDamageUI(int hp)
    {
        playerHPImages[hp].gameObject.SetActive(false);
    }

    public void UpdateOrangeMushroomUI(int count, bool isSelected)
    {
        orangeMushroomText.text = $"X{count}";
        orangeMushroomImage.sprite = isSelected ? orangeMushroomSprite[0] : orangeMushroomSprite[1];
    }

    public void UpdateBlueMushroomUI(int count, bool isSelected)
    {
        blueMushroomText.text = $"X{count}";
        blueMushroomImage.sprite = isSelected ? blueMushroomSpirte[0] : blueMushroomSpirte[1];
    }
}
