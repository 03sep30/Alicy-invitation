using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blindness : StatusEffect
{
    public GameObject BlindnessImage;
    public float effectTime = 30f;

    public override StatusEffect ApplyEffect()
    {
        BlindnessImage.SetActive(true);

        return this;
    }

    public override void RemoveEffect()
    {
        BlindnessImage.SetActive(false);
    }

    public override IEnumerator EffectTime()
    {
        yield return new WaitForSeconds(effectTime);
        RemoveEffect();
    }
}
