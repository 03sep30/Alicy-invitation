using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StatusEffect : MonoBehaviour
{
    public int effectID;
    public Sprite statusEffectSprite;

    public abstract StatusEffect ApplyEffect();
    public abstract void RemoveEffect();
    public abstract IEnumerator EffectTime();
}
