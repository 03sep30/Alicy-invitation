using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MushroomType
{
    Blue,
    Orange
}

public class PlayerMushroomHandler : MonoBehaviour
{
    private PlayerUI playerUI;
    private PlayerController playerController;

    public bool mushroomState = true;  // true = Orange, false = Blue

    private void Start()
    {
        playerUI = GetComponent<PlayerUI>();
        playerController = GetComponent<PlayerController>();
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
        playerController.currentMushroom =
            playerController.currentMushroom == MushroomType.Orange ? MushroomType.Blue : MushroomType.Orange;

        mushroomState = playerController.currentMushroom == MushroomType.Orange;

        UpdateMushroomUI();
    }

    public void GetMushroom(Mushroom mushroom)
    {
        mushroom.GetMushroom(playerController);
        UpdateMushroomUI();
    }

    private void UpdateMushroomUI()
    {
        bool isOrange = playerController.currentMushroom == MushroomType.Orange;
        bool isBlue = playerController.currentMushroom == MushroomType.Blue;

        playerUI.UpdateOrangeMushroomUI(playerController.orangeMushroomCount, isOrange);
        playerUI.UpdateBlueMushroomUI(playerController.blueMushroomCount, isBlue);
    }
}
