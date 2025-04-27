using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum ChefState
{
    Idle,
    Walking,
    Attack,
    Swing,
    Die
}

public class ChefController : MonoBehaviour
{
    public ChefState currentState;
    public Transform target;
    public float magn = 1.5f;

    private NavMeshAgent agent;
    private PlayerHealth player;
    private ChefAttack chefAttack;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = FindObjectOfType<PlayerHealth>();
        chefAttack = GetComponent<ChefAttack>();

        currentState = ChefState.Idle;
    }

    void Update()
    {
        if (!player.isDie)
        {
            MoveToTarget();
            if (chefAttack != null && chefAttack.enabled)
                chefAttack.ChefAttackPatt();
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
}
