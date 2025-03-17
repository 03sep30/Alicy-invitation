using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debuff : StatusEffect
{
    public override void ApplyEffect()
    {
        Debug.Log($"Apply {effectID}Debuff");
    }

    public override void RemoveEffect()
    {
        Debug.Log($"Remove {effectID}Debuff");
    }
}
