using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StatusEffect : MonoBehaviour
{
    public int effectID;

    public abstract void ApplyEffect();
    public abstract void RemoveEffect();
}
