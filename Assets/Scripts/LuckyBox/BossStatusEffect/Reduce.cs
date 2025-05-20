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
        statusEffectText.text = "시간 단축";

        return this;
    }

    public override void RemoveEffect()
    {
        StartCoroutine(TextTime());
    }

    public override IEnumerator EffectTime()
    {
        bossHP.DecreaseBossHP(reduceValue);
        yield return new WaitForSeconds(0);
        RemoveEffect();
    }

    public override IEnumerator TextTime()
    {
        yield return new WaitForSeconds(1.5f);
        statusEffectText.text = "";
    }
}
