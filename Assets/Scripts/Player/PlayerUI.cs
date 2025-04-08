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
    public Sprite[] orangeMushroomSprite;
    public Sprite[] blueMushroomSpirte;

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

    public void UpdateOrangeMushroomUI(int count, bool isEnable)
    {
        orangeMushroomText.text = $"X{count}";

        if (isEnable)
        {
            orangeMushroomImage.sprite = orangeMushroomSprite[0];
        }
        else
        {
            orangeMushroomImage.sprite = orangeMushroomSprite[1];
        }
    }
    public void UpdateBlueMushroomUI(int count, bool isEnable)
    {
        blueMushroomText.text = $"X{count}";

        if (isEnable)
        {
            blueMushroomImage.sprite = blueMushroomSpirte[0];
        }
        else
        {
            blueMushroomImage.sprite = blueMushroomSpirte[1];
        }
    }
}
