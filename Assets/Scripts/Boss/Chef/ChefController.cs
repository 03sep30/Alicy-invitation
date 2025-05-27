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

    public GameObject Key;
    public float keyActiveTime;
    public GameObject[] GingerCookieTotems;

    private NavMeshAgent agent;
    private PlayerHealth player;
    private ChefAttack chefAttack;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = FindObjectOfType<PlayerHealth>();
        chefAttack = GetComponent<ChefAttack>();

        currentState = ChefState.Idle;

        StartCoroutine(KeyActive());

        foreach (GameObject gingerCookie in GingerCookieTotems)
        {
            gingerCookie.SetActive(true);
        }
    }

    void Update()
    {
        if (!player.bossStage)
            return;

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

    private IEnumerator KeyActive()
    {
        yield return new WaitForSeconds(keyActiveTime);
        Key.SetActive(true);
    }

    private void OnDestroy()
    {
        foreach (GameObject gingerCookie in GingerCookieTotems)
        {
            Destroy(gingerCookie);
        }
    }
}
