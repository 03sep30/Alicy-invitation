using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reduce : StatusEffect
{
    public BossHP bossHP;
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
        if (bossHP == null)
        {
            bossHP = FindObjectOfType<BossHP>();
        }
        bossHP.DecreaseBossHP(reduceValue);
        yield return new WaitForSeconds(0);
        RemoveEffect();
    }
}
