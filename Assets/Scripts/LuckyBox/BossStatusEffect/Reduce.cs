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
        

        return this;
    }

    public override void RemoveEffect()
    {
        
    }

    public override IEnumerator EffectTime()
    {
        bossHP.DecreaseBossHP(reduceValue);
        yield return new WaitForSeconds(0);
        RemoveEffect();
    }
}
