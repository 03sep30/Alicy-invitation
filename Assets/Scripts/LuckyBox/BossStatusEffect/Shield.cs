using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : StatusEffect
{
    private PlayerHealth playerHealth;

    void Start()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
    }

    public override StatusEffect ApplyEffect()
    {
        Debug.Log(this.name);
        statusEffectText.text = "1È¸ ¹æ¾î";

        return this;
    }

    public override void RemoveEffect()
    {
        StartCoroutine(TextTime());   
    }

    public override IEnumerator EffectTime()
    {
        playerHealth.shield = true;
        yield return new WaitForSeconds(0);
        RemoveEffect();
    }

    public override IEnumerator TextTime()
    {
        yield return new WaitForSeconds(1.5f);
        statusEffectText.text = "";
    }
}
