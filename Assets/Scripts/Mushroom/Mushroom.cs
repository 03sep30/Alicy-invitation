using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : MonoBehaviour
{
    public MushroomType mushroomType;

    public void GetMushroom(PlayerController player)
    {
        if (mushroomType == MushroomType.Orange)
        {
            player.orangeMushroomCount++;
        }
        else if (mushroomType == MushroomType.Blue)
        {
            player.blueMushroomCount++;
        }

        Destroy(gameObject);
    }
}
