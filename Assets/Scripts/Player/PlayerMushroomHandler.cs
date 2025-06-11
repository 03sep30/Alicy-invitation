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
    public GameObject respawnLollipop;
    public Transform lollipopPos;
    public GameObject useMushroomEffect;

    private PlayerUI playerUI;
    private PlayerController playerController;
    private PlayerHealth playerHealth;

    private void Start()
    {
        playerUI = GetComponent<PlayerUI>();
        playerController = GetComponent<PlayerController>();
        playerHealth = GetComponentInChildren<PlayerHealth>();

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

            if (useMushroomEffect.activeInHierarchy == true)
                useMushroomEffect.SetActive(false);
            useMushroomEffect.SetActive(true);
            StartCoroutine(effectTime());
            playerController.blueMushroomCount--;
        }

        if (playerController.currentMushroom == MushroomType.Orange && playerController.orangeMushroomCount > 0)
        {
            if (playerController.currentSize == CharacterSize.Big || playerController.currentSize == CharacterSize.Normal)
                playerController.currentSize = CharacterSize.Big;
            else if (playerController.currentSize == CharacterSize.Small)
                playerController.currentSize = CharacterSize.Normal;

            if (useMushroomEffect.activeInHierarchy == true)
                useMushroomEffect.SetActive(false);
            useMushroomEffect.SetActive(true);
            StartCoroutine(effectTime());
            playerController.orangeMushroomCount--;
        }
        
        if (playerController.currentMushroom == MushroomType.Green && playerController.greenMushroomCount > 0)
        {
            Vector3 lollipopPos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 2, gameObject.transform.position.z);
            GameObject respawnLolli = Instantiate(respawnLollipop, lollipopPos, Quaternion.identity);
            playerHealth.SpawnPoint = respawnLolli.transform;

            if (useMushroomEffect.activeInHierarchy == true)
                useMushroomEffect.SetActive(false);
            useMushroomEffect.SetActive(true);
            StartCoroutine(effectTime());
            playerController.greenMushroomCount--;
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

        switch (playerController.currentMushroom)
        {
            case MushroomType.Orange:
                playerUI.ShowMushroomEffect(playerUI.orangeMushroomImage);
                break;
            case MushroomType.Blue:
                playerUI.ShowMushroomEffect(playerUI.blueMushroomImage);
                break;
            case MushroomType.Green:
                playerUI.ShowMushroomEffect(playerUI.greenMushroomImage);
                break;
        }


        Debug.Log(playerController.currentMushroom);
    }


    public void GetMushroom()
    {
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

    private IEnumerator effectTime()
    {
        yield return new WaitForSeconds(0.6f);
        useMushroomEffect.SetActive(false);
    }
}
