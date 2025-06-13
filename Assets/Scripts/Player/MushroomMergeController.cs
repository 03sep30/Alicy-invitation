using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

public class MushroomMergeController : MonoBehaviour
{
    public GameObject mergePanel;

    public bool mergeOpen = false;

    private PlayerController playerController;
    private PlayerMushroomHandler mushroomHandler;
    private StarterAssetsInputs _input;

    void Start()
    {
        _input = GetComponent<StarterAssetsInputs>();
        playerController = GetComponent<PlayerController>();
        mushroomHandler = GetComponent<PlayerMushroomHandler>();
    }

    void Update()
    {
        //if (!mergeOpen) return;

        if (Input.GetKeyDown(KeyCode.O))
        {
            mergePanel.SetActive(true);

            mergePanel.gameObject.SetActive(!playerController.isPanelActive);
            playerController.isPanelActive = mergePanel.activeSelf;

            _input.SetCursorState(!playerController.isPanelActive);
        }
    }

    public void MushroomMerge()
    {
        if (GameManager.Instance.orangeMushroomCount >= 10)
        {
            GameManager.Instance.greenMushroomCount++;
            GameManager.Instance.orangeMushroomCount -= 10;
            mushroomHandler.UpdateMushroomUI();
        }
    }
}
