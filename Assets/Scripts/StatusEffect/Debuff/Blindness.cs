using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blindness : StatusEffect
{
    public GameObject BlindnessImage;

    public override void ApplyEffect()
    {
        BlindnessImage.SetActive(true);
    }

    public override void RemoveEffect()
    {
        BlindnessImage.SetActive(false);
    }
}
