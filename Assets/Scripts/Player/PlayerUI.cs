using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public Image[] playerHPImages;

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
}
