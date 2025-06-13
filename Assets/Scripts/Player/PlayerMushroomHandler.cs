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

        GameManager.Instance.currentMushroom = MushroomType.Orange;
        UpdateMushroomUI();
    }

    public void UseMushroom()
    {
        if (GameManager.Instance.currentMushroom == MushroomType.Blue && GameManager.Instance.blueMushroomCount > 0)
        {
            if (GameManager.Instance.currentSize == CharacterSize.Small || GameManager.Instance.currentSize == CharacterSize.Normal)
                GameManager.Instance.currentSize = CharacterSize.Small;
            else if (GameManager.Instance.currentSize == CharacterSize.Big)
                GameManager.Instance.currentSize = CharacterSize.Normal;

            if (useMushroomEffect.activeInHierarchy == true)
                useMushroomEffect.SetActive(false);
            useMushroomEffect.SetActive(true);
            StartCoroutine(effectTime());
            GameManager.Instance.blueMushroomCount--;
        }

        if (GameManager.Instance.currentMushroom == MushroomType.Orange && GameManager.Instance.orangeMushroomCount > 0)
        {
            if (GameManager.Instance.currentSize == CharacterSize.Big || GameManager.Instance.currentSize == CharacterSize.Normal)
                GameManager.Instance.currentSize = CharacterSize.Big;
            else if (GameManager.Instance.currentSize == CharacterSize.Small)
                GameManager.Instance.currentSize = CharacterSize.Normal;

            if (useMushroomEffect.activeInHierarchy == true)
                useMushroomEffect.SetActive(false);
            useMushroomEffect.SetActive(true);
            StartCoroutine(effectTime());
            GameManager.Instance.orangeMushroomCount--;
        }
        
        if (GameManager.Instance.currentMushroom == MushroomType.Green && GameManager.Instance.greenMushroomCount > 0)
        {
            Vector3 lollipopPos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 2, gameObject.transform.position.z);
            GameObject respawnLolli = Instantiate(respawnLollipop, lollipopPos, Quaternion.identity);
            playerHealth.SpawnPoint = respawnLolli.transform;

            if (useMushroomEffect.activeInHierarchy == true)
                useMushroomEffect.SetActive(false);
            useMushroomEffect.SetActive(true);
            StartCoroutine(effectTime());
            GameManager.Instance.greenMushroomCount--;
        }

        UpdateMushroomUI();
    }


    public void SwapMushroom()
    {
        GameManager.Instance.currentMushroom = GameManager.Instance.currentMushroom switch
        {
            MushroomType.Orange => MushroomType.Blue,
            MushroomType.Blue => MushroomType.Green,
            MushroomType.Green => MushroomType.Orange,
            _ => MushroomType.Orange
        };

        UpdateMushroomUI();

        switch (GameManager.Instance.currentMushroom)
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


        Debug.Log(GameManager.Instance.currentMushroom);
    }


    public void GetMushroom()
    {
        UpdateMushroomUI();
    }

    public void UpdateMushroomUI()
    {
        bool isOrange = GameManager.Instance.currentMushroom == MushroomType.Orange;
        bool isBlue = GameManager.Instance.currentMushroom == MushroomType.Blue;
        bool isGreen = GameManager.Instance.currentMushroom == MushroomType.Green;

        playerUI.UpdateOrangeMushroomUI(GameManager.Instance.orangeMushroomCount, isOrange);
        playerUI.UpdateBlueMushroomUI(GameManager.Instance.blueMushroomCount, isBlue);
        playerUI.UpdateGreenMushroomUI(GameManager.Instance.greenMushroomCount, isGreen);
    }

    private IEnumerator effectTime()
    {
        yield return new WaitForSeconds(0.6f);
        useMushroomEffect.SetActive(false);
    }
}
