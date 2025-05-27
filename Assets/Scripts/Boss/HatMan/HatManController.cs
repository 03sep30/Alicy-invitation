using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum HatManState
{
    Idle,
    Walking,
    Attack,
    Swing,
    Die
}

public class HatManController : MonoBehaviour
{
    public HatManState currentState;

    private PlayerHealth player;
    private HatManAttack hatManAttack;

    void Start()
    {
        player = FindObjectOfType<PlayerHealth>();
        hatManAttack = GetComponent<HatManAttack>();

        currentState = HatManState.Idle;
    }

    void Update()
    {
        if (!player.bossStage)
            return;
        if (!player.isDie)
        {
            if (hatManAttack != null && hatManAttack.enabled)
                hatManAttack.HatAttackPatt();
        }
    }
}
