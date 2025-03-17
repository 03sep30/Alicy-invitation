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

    public override void ApplyEffect()
    {
        Debug.Log(this.name);

        var player = FindObjectOfType<ThirdPersonController>();

        originalMoveSpeed = player.MoveSpeed;
        originalSprintSpeed = player.SprintSpeed;
        originalJumpHeight = player.JumpHeight;

        player.MoveSpeed = 2;
        player.SprintSpeed = 2;
        player.JumpHeight = originalJumpHeight / 2;
        Debug.Log("qwe");
    }
    public override void RemoveEffect()
    {
        player.MoveSpeed = originalMoveSpeed;
        player.SprintSpeed = originalSprintSpeed;
        player.JumpHeight = originalJumpHeight;
    }
}
