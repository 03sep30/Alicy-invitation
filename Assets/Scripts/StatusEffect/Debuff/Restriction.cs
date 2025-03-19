using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

public class Restriction : StatusEffect
{
    private ThirdPersonController player;

    private float originalMoveSpeed;
    private float originalSprintSpeed;
    private float originalJumpHeight;

    void Start()
    {
        player = FindObjectOfType<ThirdPersonController>();
    }

    public override StatusEffect ApplyEffect()
    {
        originalMoveSpeed = player.MoveSpeed;
        originalSprintSpeed = player.SprintSpeed;
        originalJumpHeight = player.JumpHeight;

        player.MoveSpeed = 2;
        player.SprintSpeed = 2;
        player.JumpHeight = originalJumpHeight / 2;

        return this;
    }
    public override void RemoveEffect()
    {
        player.MoveSpeed = originalMoveSpeed;
        player.SprintSpeed = originalSprintSpeed;
        player.JumpHeight = originalJumpHeight;
    }
}
