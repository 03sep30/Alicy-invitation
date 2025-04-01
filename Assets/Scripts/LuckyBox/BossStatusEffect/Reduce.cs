using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reduce : StatusEffect
{
    private BossHP bossHP;
    public float reduceValue;

    void Start()
    {
        bossHP = FindObjectOfType<BossHP>();
    }

    public override StatusEffect ApplyEffect()
    {
        Debug.Log(this.name);

        bossHP.DecreaseBossHP(reduceValue);

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
