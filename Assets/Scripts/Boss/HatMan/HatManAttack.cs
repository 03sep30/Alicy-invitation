using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatManAttack : BossAttack
{
    public Transform target;

    [Header("Hat")]
    [SerializeField] private float dropForce;
    [SerializeField] private float potDistance;
    public GameObject hatPrefab;

    [Header("LightPillar")]
    [SerializeField] private float lightPillarOffset;
    public GameObject lightPillarPrefab;
    public float lightPillarRandomMinOffsetX;
    public float lightPillarRandomMaxOffsetX;
    public float lightPillarRandomMinOffsetZ;
    public float lightPillarRandomMaxOffsetZ;

    [Header("Attack")]
    public float attackInterval = 3f;
    public float currentAttackTime;
    public int attackIndex = 0;
    [SerializeField] private int[] attackPattern = { 0, 1, 1 };

    void Start()
    {
        if (target == null)
        {
            target = FindObjectOfType<PlayerController>().gameObject.transform;
        }
        currentAttackTime = attackInterval;
    }

    public void HatAttackPatt()
    {
        currentAttackTime -= Time.deltaTime;

        if (currentAttackTime <= 0)
        {
            int pattern = attackPattern[attackIndex];

            switch (pattern)
            {
                case 0:
                    DropHatTrap();
                    break;
                case 1:
                    CreateLightPillar();
                    break;
            }

            attackIndex = (attackIndex + 1) % attackPattern.Length;
            currentAttackTime = attackInterval;
        }
    }


    public void DropHatTrap()
    {
        Vector3 targetPos = target.position;
        GameObject hat = Instantiate(hatPrefab, new Vector3(targetPos.x,
            targetPos.y + potDistance, targetPos.z), Quaternion.identity);
        //hat.transform.localRotation = Quaternion.Euler(0f, 0f, 180f);
        Rigidbody rb = hat.GetComponent<Rigidbody>();
        Vector3 direction = Vector3.down;
        rb.AddForce(direction * dropForce, ForceMode.Impulse);

        attackIndex = (attackIndex + 1) % attackPattern.Length;
    }

    public void CreateLightPillar()
    {
        Vector3 targetPos = target.position;
        //Vector3 moveDir = target.forward * lightPillarRandomOffset;
        //Vector3 spawnPos = targetPos + moveDir.normalized;

        Vector3 randomOffset = new Vector3(
            Random.Range(lightPillarRandomMinOffsetX, lightPillarRandomMaxOffsetX),
            targetPos.y,
            Random.Range(lightPillarRandomMinOffsetZ, lightPillarRandomMaxOffsetZ));
        Vector3 spawnPos = new Vector3(
            targetPos.x + randomOffset.x, targetPos.y, targetPos.z + randomOffset.z);

        GameObject firePillar = Instantiate(lightPillarPrefab, new Vector3(spawnPos.x, spawnPos.y, spawnPos.z), Quaternion.identity);
        Debug.Log(spawnPos);
        attackIndex = (attackIndex + 1) % attackPattern.Length;
    }
}
