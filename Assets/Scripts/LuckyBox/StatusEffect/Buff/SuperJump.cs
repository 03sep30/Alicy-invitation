using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

public class SuperJump : StatusEffect
{
    private ThirdPersonController player;
    public float effectTime = 30f;

    private float originalJumpHeight;

    void Start()
    {
        player = FindObjectOfType<ThirdPersonController>();
    }

    public override StatusEffect ApplyEffect()
    {
        Debug.Log(this.name);

        originalJumpHeight = player.JumpHeight;
        
        player.JumpHeight = originalJumpHeight * 2;

        return this;
    }

    public override void RemoveEffect()
    {
        player.JumpHeight = originalJumpHeight;
    }

    public override IEnumerator EffectTime()
    {
        yield return new WaitForSeconds(effectTime);
        RemoveEffect();
    }

    public override IEnumerator TextTime()
    {
        yield return new WaitForSeconds(1.5f);
        statusEffectText.text = "";
    }
}
