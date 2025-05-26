using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stun : StatusEffect
{
    public float stunTime;
    private BossAttack boss;
    private Color originalColor;
    private SkinnedMeshRenderer skinnedMeshRenderer;

    void Start()
    {
        boss = FindObjectOfType<BossAttack>();
    }

    public override StatusEffect ApplyEffect()
    {
        Debug.Log(this.name);
        statusEffectText.text = "보스 움직임 제한";

        //boss.GetComponent<Animator>().enabled = false;
        switch (boss)
        {
            case CheshireAttack:
                var cheshire = boss as CheshireAttack;
                cheshire.attackActive = false;
                break;
        }
        return this;
    }

    public override void RemoveEffect()
    {
        //boss.GetComponent<Animator>().enabled = true;
        switch (boss)
        {
            case CheshireAttack:
                var cheshire = boss as CheshireAttack;
                if (skinnedMeshRenderer != null)
                {
                    skinnedMeshRenderer = cheshire.GetComponent<SkinnedMeshRenderer>();
                    foreach (var cheshireMR in skinnedMeshRenderer.materials)
                    {
                        cheshireMR.SetColor("_BaseColor", originalColor);
                    }
                }
                cheshire.attackActive = true;
                break;
        }
    }

    public override IEnumerator EffectTime()
    {
        switch (boss)
        {
            case CheshireAttack:
                var cheshire = boss as CheshireAttack;
                skinnedMeshRenderer = boss.GetComponent<SkinnedMeshRenderer>();
                foreach (var cheshireMR in skinnedMeshRenderer.materials)
                {
                    originalColor = cheshireMR.GetColor("_BaseColor");
                    cheshireMR.SetColor("_BaseColor", Color.yellow);
                }
                break;
        }
        yield return new WaitForSeconds(stunTime);
        RemoveEffect();
    }
}
