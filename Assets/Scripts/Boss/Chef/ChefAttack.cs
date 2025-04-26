using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChefAttack : BossAttack
{
    public GameObject potPrefab;
    public Transform target;
    [SerializeField]
    private float dropForce;
    [SerializeField]
    private float distance;

    public void DropPotTrap()
    {
        Vector3 targetPos = target.position;
        GameObject pot = Instantiate(potPrefab, new Vector3(targetPos.x, 
            targetPos.y + distance, targetPos.z), Quaternion.identity);
        pot.transform.localRotation = Quaternion.Euler(0f, 0f, 180f);
        Rigidbody rb = pot.GetComponent<Rigidbody>();
        Vector3 direction = Vector3.down;
        rb.AddForce(direction * dropForce, ForceMode.Impulse);
    }

    //public void 
}
