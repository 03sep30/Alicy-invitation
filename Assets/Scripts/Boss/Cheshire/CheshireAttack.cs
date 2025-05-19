using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

public enum CheshireAttackType
{
    Punch,
    Hiss,
    BigHiss
}

public class CheshireAttack : BossAttack
{
    [SerializeField] private float attackInterval;
    private float currentAttackTime;
    public Transform target;
    public bool isPreparingAttack = false;
    public bool isHissing = false;
    private int attackNum;

    public CheshireAttackType currentAttackType;

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
            attackNum = Random.Range(0, System.Enum.GetValues(typeof(CheshireAttackType)).Length);
            currentAttackType = (CheshireAttackType)attackNum;

            switch (currentAttackType)
            {
                case CheshireAttackType.Punch:
                    Debug.Log("펀치 공격");
                    Attack(playerHealth);
                    break;

                case CheshireAttackType.Hiss:
                    Debug.Log("하악질 공격");
                    HighlightPlatform();
                    break;

                case CheshireAttackType.BigHiss:
                    Debug.Log("더 큰 하악질 공격");
                    HighlightPlatform();
                    break;
            }
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
            }
        }
    }

    private IEnumerator Hiss()
    {
        if (highlightPlatform != null && currentAttackType == CheshireAttackType.Hiss)
        {
            Collider platformCollider = highlightPlatform.GetComponent<Collider>();

            if (platformCollider.bounds.Contains(target.position) && thirdPersonController.Grounded)
            {
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
        if (platforms.Length > 0 && currentAttackType == CheshireAttackType.BigHiss)
        {
            foreach (var platform in platforms)
            {
                Collider platformCollider = platform.GetComponent<Collider>();
                MeshRenderer platformMat = platform.GetComponent<MeshRenderer>();

                if (platformCollider.bounds.Contains(target.position) && thirdPersonController.Grounded)
                {
                    if (playerHealth != null)
                    {
                        playerHealth.TakeDamage(1);
                    }
                }
            }
            yield return new WaitForSeconds(0.5f);
        }
    }

    void HighlightPlatform()
    {
        distance = Mathf.Infinity;
        highlightPlatform = null;

        foreach (var platform in platforms)
        {
            float _distance = Vector3.Distance(target.position, platform.transform.position);

            if (distance == 0)
                distance = _distance;
            if (distance > _distance)
            {
                distance = _distance;
                highlightPlatform = platform;
            }
        }

        StartCoroutine(HighlightPlatformTime());
    }

    private IEnumerator HighlightPlatformTime()
    {
        if (currentAttackType == CheshireAttackType.BigHiss)
        {
            foreach (var platform in platforms)
            {
                MeshRenderer platformMat = platform.GetComponent<MeshRenderer>();
                if (platformMat != null)
                {
                    platformMat.material.color = Color.red;
                }
            }

            yield return new WaitForSeconds(highlightTime);

            foreach (var platform in platforms)
            {
                MeshRenderer platformMat = platform.GetComponent<MeshRenderer>();
                if (platformMat != null)
                {
                    platformMat.material.color = Color.white;
                }
            }

            PerformHiss();
        }

        else if (highlightPlatform != null)
        {
            MeshRenderer platformMat = highlightPlatform.GetComponent<MeshRenderer>();

            platformMat.material.color = Color.red;
            yield return new WaitForSeconds(highlightTime);
            platformMat.material.color = Color.white;

            PerformHiss();
        }
    }
}