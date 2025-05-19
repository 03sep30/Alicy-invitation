using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MushroomType
{
    Blue,
    Orange,
    Green
}

public class PlayerMushroomHandler : MonoBehaviour
{
    private PlayerUI playerUI;
    private PlayerController playerController;

    private void Start()
    {
        playerUI = GetComponent<PlayerUI>();
        playerController = GetComponent<PlayerController>();
        playerController.currentMushroom = MushroomType.Orange;
        UpdateMushroomUI();
    }

    public void UseMushroom()
    {
        if (playerController.currentMushroom == MushroomType.Blue && playerController.blueMushroomCount > 0)
        {
            if (playerController.currentSize == CharacterSize.Small || playerController.currentSize == CharacterSize.Normal)
                playerController.currentSize = CharacterSize.Small;
            else if (playerController.currentSize == CharacterSize.Big)
                playerController.currentSize = CharacterSize.Normal;

            playerController.blueMushroomCount--;
        }

        if (playerController.currentMushroom == MushroomType.Orange && playerController.orangeMushroomCount > 0)
        {
            if (playerController.currentSize == CharacterSize.Big || playerController.currentSize == CharacterSize.Normal)
                playerController.currentSize = CharacterSize.Big;
            else if (playerController.currentSize == CharacterSize.Small)
                playerController.currentSize = CharacterSize.Normal;

            playerController.orangeMushroomCount--;
        }

        UpdateMushroomUI();
    }


    public void SwapMushroom()
    {
        playerController.currentMushroom = playerController.currentMushroom switch
        {
            MushroomType.Orange => MushroomType.Blue,
            MushroomType.Blue => MushroomType.Green,
            MushroomType.Green => MushroomType.Orange,
            _ => MushroomType.Orange
        };

        UpdateMushroomUI();
        Debug.Log(playerController.currentMushroom);
    }


    public void GetMushroom(Mushroom mushroom)
    {
        mushroom.GetMushroom(playerController);
        UpdateMushroomUI();
    }

    public void UpdateMushroomUI()
    {
        bool isOrange = playerController.currentMushroom == MushroomType.Orange;
        bool isBlue = playerController.currentMushroom == MushroomType.Blue;
        bool isGreen = playerController.currentMushroom == MushroomType.Green;

        playerUI.UpdateOrangeMushroomUI(playerController.orangeMushroomCount, isOrange);
        playerUI.UpdateBlueMushroomUI(playerController.blueMushroomCount, isBlue);
        playerUI.UpdateGreenMushroomUI(playerController.greenMushroomCount, isGreen);
    }
}
