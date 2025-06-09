using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : MonoBehaviour
{
    public MushroomType mushroomType;

    public void GetMushroom(PlayerController player)
    {
        if (mushroomType == MushroomType.Blue)
        {
            player.blueMushroomCount++;
            player.mushroomEffects[0].SetActive(true);
        }
        if (mushroomType == MushroomType.Orange)
        {
            player.orangeMushroomCount++;
            player.mushroomEffects[1].SetActive(true);
        }
        if (mushroomType == MushroomType.Green)
        {
            player.greenMushroomCount++;
            player.mushroomEffects[2].SetActive(true);
        }

        DestroyObject destroyObject = gameObject.GetComponentInChildren<DestroyObject>();
        destroyObject.StartDestroy(gameObject, 0f);
        Destroy(gameObject);
    }
}
