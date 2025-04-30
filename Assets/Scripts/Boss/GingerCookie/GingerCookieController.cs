using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum GingerCookieState
{
    Idle,
    Walking,
    Attack,
    Swing,
    Die
}

public class GingerCookieController : MonoBehaviour
{
    public GingerCookieState currentState;
    public Transform target;
    public float magn = 1.5f;
    public float range;

    public GameObject portal;
    public float portalActiveTime;

    public bool isTotem = false;

    private NavMeshAgent agent;
    private PlayerHealth player;
    private GingerCookieAttack gingerAttack;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = FindObjectOfType<PlayerHealth>();
        gingerAttack = GetComponent<GingerCookieAttack>();

        currentState = GingerCookieState.Idle;

        if (!isTotem && portal != null)
        StartCoroutine(PortalActive());
    }

    void Update()
    {
        if (!player.isDie)
        {
            float distance = Vector3.Distance(transform.position, target.position);
            
            MoveToTarget();
            if (gingerAttack != null && gingerAttack.enabled && distance <= range)
                gingerAttack.GingerAttack();
        }
    }

    void MoveToTarget()
    {
        if (target == null) return;

        Vector3 direction = target.position - transform.position;
        direction.y = 0;

        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);

        if (direction.magnitude > magn && !isTotem && agent != null)
        {
            Vector3 targetPos = new Vector3(target.position.x + magn, target.position.y, target.position.z + magn);
            
            agent.SetDestination(targetPos);
        }
    }

    private IEnumerator PortalActive()
    {
        yield return new WaitForSeconds(portalActiveTime);
        portal.SetActive(true);
    }
}
