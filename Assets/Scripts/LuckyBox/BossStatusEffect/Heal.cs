using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : StatusEffect
{
    public int heal;
    private PlayerHealth playerHealth;

    void Start()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
    }

    public override StatusEffect ApplyEffect()
    {
        Debug.Log(this.name);
        statusEffectText.text = "���� ȸ��";

        return this;
    }

    public override void RemoveEffect()
    {
        
    }

    public override IEnumerator EffectTime()
    {
        playerHealth.PlayerHeal(heal);
        yield return new WaitForSeconds(0f);
        RemoveEffect();
    }
}
