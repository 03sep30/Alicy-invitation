using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChefAttack : BossAttack
{
    public Transform target;

    [Header("Pot")]
    [SerializeField] private float dropForce;
    [SerializeField] private float potDistance;
    public GameObject potPrefab;

    [Header("FirePillar")]
    [SerializeField]
    private float firePillarOffset;
    public GameObject firePillarPrefab;
    public float firePillarRandomMinOffsetX;
    public float firePillarRandomMaxOffsetX;
    public float firePillarRandomMinOffsetZ;
    public float firePillarRandomMaxOffsetZ;

    [Header("Attack")]
    public float attackInterval = 3f;
    public float currentAttackTime;
    public int attackIndex = 0;
    [SerializeField] private int[] attackPattern = { 0, 1, 1 };

    void Start()
    {
        currentAttackTime = attackInterval;
    }

    public void ChefAttackPatt()
    {
        currentAttackTime -= Time.deltaTime;

        if (currentAttackTime <= 0)
        {
            int pattern = attackPattern[attackIndex];

            switch (pattern)
            {
                case 0:
                    DropPotTrap();
                    break;
                case 1:
                    CreateFirePillar();
                    break;
            }

            attackIndex = (attackIndex + 1) % attackPattern.Length;
            currentAttackTime = attackInterval;
        }
    }


    public void DropPotTrap()
    {
        Vector3 targetPos = target.position;
        GameObject pot = Instantiate(potPrefab, new Vector3(targetPos.x,
            targetPos.y + potDistance, targetPos.z), Quaternion.identity);
        //pot.transform.localRotation = Quaternion.Euler(0f, 0f, 180f);
        Rigidbody rb = pot.GetComponent<Rigidbody>();
        Vector3 direction = Vector3.down;
        rb.AddForce(direction * dropForce, ForceMode.Impulse);

        attackIndex = (attackIndex + 1) % attackPattern.Length;
    }

    public void CreateFirePillar()
    {
        Vector3 targetPos = target.position;

        Vector3 randomOffset = new Vector3(
           Random.Range(firePillarRandomMinOffsetX, firePillarRandomMaxOffsetX),
           targetPos.y,
           Random.Range(firePillarRandomMinOffsetZ, firePillarRandomMaxOffsetZ));
        Vector3 spawnPos = new Vector3(
            targetPos.x + randomOffset.x, targetPos.y, targetPos.z + randomOffset.z);

        GameObject firePillar = Instantiate(firePillarPrefab, new Vector3(spawnPos.x, spawnPos.y, spawnPos.z), Quaternion.identity);

        attackIndex = (attackIndex + 1) % attackPattern.Length;
    }
}
