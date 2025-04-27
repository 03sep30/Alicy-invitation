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

    public GameObject portal;
    public float portalActiveTime;

    private NavMeshAgent agent;
    private PlayerHealth player;
    private GingerCookieAttack gingerAttack;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = FindObjectOfType<PlayerHealth>();
        gingerAttack = GetComponent<GingerCookieAttack>();

        currentState = GingerCookieState.Idle;

        StartCoroutine(PortalActive());
    }

    void Update()
    {
        if (!player.isDie)
        {
            MoveToTarget();
            if (gingerAttack != null && gingerAttack.enabled)
                gingerAttack.GingerAttack();
        }
    }
    void MoveToTarget()
    {
        if (target == null) return;

        Vector3 direction = target.position - transform.position;
        direction.y = 0;
        if (direction.magnitude > magn)
        {
            Vector3 targetPos = new Vector3(target.position.x + magn, target.position.y, target.position.z + magn);
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
            agent.SetDestination(targetPos);
        }
    }

    private IEnumerator PortalActive()
    {
        yield return new WaitForSeconds(portalActiveTime);
        portal.SetActive(true);
    }
}
