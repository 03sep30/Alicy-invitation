using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public abstract class StatusEffect : MonoBehaviour
{
    public int effectID;
    public TextMeshProUGUI statusEffectText;

    public abstract StatusEffect ApplyEffect();
    public abstract void RemoveEffect();
    public abstract IEnumerator EffectTime();
}
