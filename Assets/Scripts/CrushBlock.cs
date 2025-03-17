using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrushBlock : MonoBehaviour
{
    private Coroutine stayCoroutine = null;
    public float CrushTime = 0.5f;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && stayCoroutine == null)
        {
            stayCoroutine = StartCoroutine(StayTimer(other.GetComponentInChildren<PlayerHealth>()));
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && stayCoroutine != null)
        {
            StopCoroutine(stayCoroutine);
            stayCoroutine = null;
        }
    }

    IEnumerator StayTimer(PlayerHealth player)
    {
        yield return new WaitForSeconds(CrushTime);

        Debug.Log("2초 동안 충돌 유지됨! 실행!");
        player.Die();
    }
}
