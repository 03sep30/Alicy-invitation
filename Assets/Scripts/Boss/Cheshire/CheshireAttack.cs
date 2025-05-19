using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

public class CheshireAttack : BossAttack
{
    [SerializeField] private float attackInterval;
    private float currentAttackTime;
    public Transform target;
    public bool isPreparingAttack = false;
    public bool isHissing = false;

    [Header("Platform")]
    public GameObject highlightPlatform;
    [SerializeField] private float highlightTime;
    private float distance;
    [SerializeField] private GameObject[] platforms;

    private CheshireController cheshireController;
    private ThirdPersonController thirdPersonController;
    private PlayerHealth playerHealth;

    void Start()
    {
        cheshireController = GetComponent<CheshireController>();
        thirdPersonController = FindObjectOfType<ThirdPersonController>();
        playerHealth = FindObjectOfType<PlayerHealth>();

        currentAttackTime = attackInterval;
    }

    void Update()
    {
        currentAttackTime -= Time.deltaTime;
        if (currentAttackTime <= 0)
        {
            Debug.Log("체셔 공격");
            HighlightPlatform();
            currentAttackTime = attackInterval;
        }
    }

    public override void Attack(PlayerHealth player)
    {
        base.Attack(player);
    }

    public void PerformHiss()
    {
        StartCoroutine(Hiss());
    }

    public void PerformBigHiss()
    {
        if (platforms != null)
        {
            foreach (var platform in platforms)
            {
                Collider platformCollider = platform.GetComponent<Collider>();

                if (platformCollider.bounds.Contains(target.position) && thirdPersonController.Grounded)
                {
                    if (playerHealth != null)
                    {
                        playerHealth.TakeDamage(1);
                    }
                }
                else
                {
                    Debug.Log("피함");
                }
            }
        }
    }

    private IEnumerator Hiss()
    {
        if (highlightPlatform != null)
        {
            Collider platformCollider = highlightPlatform.GetComponent<Collider>();

            if (platformCollider.bounds.Contains(target.position) && thirdPersonController.Grounded)
            {
                Debug.Log("플레이어가 하이라이트 플랫폼 위에 있어서 데미지를 받음");
                //PlayerHealth playerHealth = target.GetComponentInChildren<PlayerHealth>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(1);
                }
            }
            else
            {
                Debug.Log("플레이어가 플랫폼 위에 없음 또는 공중에 있음");
            }
            yield return new WaitForSeconds(0.5f);
        }

        //BigHiss
    }

    void HighlightPlatform()
    {
        distance = Mathf.Infinity;
        highlightPlatform = null;

        foreach (var platform in platforms)
        {
            float _distance = Vector3.Distance(target.position, platform.transform.position);

            Debug.Log($"{platform.name} : {_distance}");
            if (distance == 0)
                distance = _distance;
            if (distance > _distance)
            {
                distance = _distance;
                highlightPlatform = platform;
            }
        }

        StartCoroutine(HighlightPlatformTime());
        Debug.Log("빛나는 플랫폼");
    }

    private IEnumerator HighlightPlatformTime()
    {
        if (highlightPlatform != null)
        {
            MeshRenderer platformMat = highlightPlatform.GetComponent<MeshRenderer>();

            platformMat.material.color = Color.red;
            yield return new WaitForSeconds(highlightTime);
            platformMat.material.color = Color.white;

            PerformHiss();
        }
    }
}