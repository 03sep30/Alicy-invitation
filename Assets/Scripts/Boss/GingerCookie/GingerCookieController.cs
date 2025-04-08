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
    private NavMeshAgent agent;
    public GingerCookieState currentState;
    public Transform target;
    private PlayerHealth player;
    private GingerCookieAttack gingerAttack;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = FindObjectOfType<PlayerHealth>();
        gingerAttack = GetComponent<GingerCookieAttack>();

        currentState = GingerCookieState.Idle;

    }

    void Update()
    {
        if (!player.isDie)
        {
            MoveToTarget();
            gingerAttack.GingerAttack();
        }
        
    }
    void MoveToTarget()
    {
        if (target == null) return;

        agent.SetDestination(target.position);

        Vector3 direction = target.position - transform.position;
        direction.y = 0;
        if (direction.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
        }
    }
}
