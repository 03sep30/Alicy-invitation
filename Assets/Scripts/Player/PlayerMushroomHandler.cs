using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MushroomType
{
    Orange,
    Blue
}

public class PlayerMushroomHandler : MonoBehaviour
{
    private PlayerUI playerUI;
    private PlayerController playerController;

    private bool orangeMushroom;
    private bool blueMushroom;

    private void Start()
    {
        playerUI = gameObject.GetComponent<PlayerUI>();
        playerController = gameObject.GetComponent<PlayerController>();
    }

    public void UseMushroom()
    {
        if (playerController.currentMushroom == MushroomType.Blue && playerController.blueMushroomCount > 0)
        {
            playerController.blueMushroomCount--;
            playerUI.UpdateBlueMushroomUI(playerController.blueMushroomCount, blueMushroom);
            
        }
        if (playerController.currentMushroom == MushroomType.Orange && playerController.orangeMushroomCount > 0)
        {
            playerController.orangeMushroomCount--;
            playerUI.UpdateOrangeMushroomUI(playerController.orangeMushroomCount, orangeMushroom);
            
        }
    }

    public void SwapMushroom()
    {
        playerController.currentMushroom =
            playerController.currentMushroom == MushroomType.Orange ? MushroomType.Blue : MushroomType.Orange;

        bool isOrange = playerController.currentMushroom == MushroomType.Orange;
        bool isBlue = playerController.currentMushroom == MushroomType.Blue;

        playerUI.UpdateOrangeMushroomUI(playerController.orangeMushroomCount, isOrange);
        playerUI.UpdateBlueMushroomUI(playerController.blueMushroomCount, isBlue);
    }


}
