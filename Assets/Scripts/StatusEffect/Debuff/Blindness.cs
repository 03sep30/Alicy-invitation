using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blindness : StatusEffect
{
    public GameObject BlindnessImage;

    public override StatusEffect ApplyEffect()
    {
        BlindnessImage.SetActive(true);

        return this;
    }

    public override void RemoveEffect()
    {
        BlindnessImage.SetActive(false);
    }
}
