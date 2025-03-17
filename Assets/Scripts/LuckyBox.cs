using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuckyBox : MonoBehaviour
{
    private StatusEffect currentStatus;
    public List<StatusEffect> statusEffectList;

    public void OpenLuckyBox()
    {
        int RandomNum = Random.Range(0, statusEffectList.Count);

        currentStatus = statusEffectList[RandomNum];
        currentStatus.ApplyEffect();
    }
}
