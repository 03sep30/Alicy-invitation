using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reduce : StatusEffect
{
    private Boss boss;
    public float reduceValue;

    void Start()
    {
        boss = FindObjectOfType<Boss>();
    }

    public override StatusEffect ApplyEffect()
    {
        Debug.Log(this.name);

        boss.DecreaseBossHP(reduceValue);

        return this;
    }

    public override void RemoveEffect()
    {

    }

    public override IEnumerator EffectTime()
    {
        yield return new WaitForSeconds(0);
        RemoveEffect();
    }
}
