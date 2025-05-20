using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuckyBox : MonoBehaviour
{
    public StatusEffect currentStatus;
    public List<StatusEffect> statusEffectList;

    public float luckyBoxTime = 5f;
    private float currentLuckyBoxTime;

    void Start()
    {
        statusEffectList.AddRange(FindObjectsOfType<StatusEffect>());
        currentLuckyBoxTime = luckyBoxTime;
    }

    void Update()
    {
        currentLuckyBoxTime -= Time.deltaTime;
        if (currentLuckyBoxTime <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void OpenLuckyBox()
    {
        int RandomNum = Random.Range(0, statusEffectList.Count);

        currentStatus = statusEffectList[RandomNum];
        currentStatus.ApplyEffect();
    }
}
