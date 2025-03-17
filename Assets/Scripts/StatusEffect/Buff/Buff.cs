using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff : StatusEffect
{
    public override void ApplyEffect()
    {
        Debug.Log($"Apply {effectID}Buff");
    }

    public override void RemoveEffect()
    {
        Debug.Log($"Remove {effectID}Buff");
    }
}
