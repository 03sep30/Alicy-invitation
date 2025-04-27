using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GingerCookieAttack : BossAttack
{
    public GameObject[] lollipops;
    public Transform throwPoint;
    public float throwInterval = 3f;
    public float throwForce = 500f;

    private float currentThrowTime;
    private int throwIndex = 0;
    [SerializeField] private int[] throwPattern = { 0, 0, 1 };

    public Transform target;
    

    void Start()
    {
        currentThrowTime = throwInterval;
    }

    public void GingerAttack()
    {
        Vector3 targetPos = target.position;
        targetPos.y = throwPoint.position.y;

        Vector3 direction = (targetPos - throwPoint.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        throwPoint.rotation = Quaternion.Lerp(throwPoint.rotation, targetRotation, Time.deltaTime * 5f);

        currentThrowTime -= Time.deltaTime;

        if (currentThrowTime <= 0)
        {
            ThrowLollipop();
            currentThrowTime = throwInterval;
        }
    }

    private void ThrowLollipop()
    {
        int lollipopIndex = throwPattern[throwIndex];
        GameObject lollipopPrefab = lollipops[lollipopIndex];
        GameObject lollipop = Instantiate(lollipopPrefab, throwPoint.position, Quaternion.identity);
        Rigidbody rb = lollipop.GetComponent<Rigidbody>();

        Vector3 targetPos = target.position;
        targetPos.y = throwPoint.position.y;
        Vector3 direction = (targetPos - throwPoint.position).normalized;

        Quaternion lookRotation = Quaternion.LookRotation(direction);
        lollipop.transform.rotation = lookRotation * Quaternion.Euler(90f, 0f, 0f);

        rb.AddForce(direction * throwForce);

        throwIndex = (throwIndex + 1) % throwPattern.Length;
    }
}
