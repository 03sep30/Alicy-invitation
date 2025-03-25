using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stun : StatusEffect
{
    public float stunTime;
    private BossHP boss;

    void Start()
    {
        boss = FindObjectOfType<BossHP>();
    }

    public override StatusEffect ApplyEffect()
    {
        Debug.Log(this.name);

        boss.GetComponent<Animator>().enabled = false;

        return this;
    }

    public override void RemoveEffect()
    {
        boss.GetComponent<Animator>().enabled = true;
    }

    public override IEnumerator EffectTime()
    {
        yield return new WaitForSeconds(stunTime);
        RemoveEffect();
    }
}
