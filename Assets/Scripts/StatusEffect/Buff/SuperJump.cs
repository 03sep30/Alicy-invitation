using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

public class SuperJump : StatusEffect
{
    private ThirdPersonController player;

    private float originalJumpHeight;

    public override void ApplyEffect()
    {
        Debug.Log(this.name);

        player = FindObjectOfType<ThirdPersonController>();

        originalJumpHeight = player.JumpHeight;
        
        player.JumpHeight = originalJumpHeight * 2;
    }

    public override void RemoveEffect()
    {
        player.JumpHeight = originalJumpHeight;
    }
}
