using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorButton : MonoBehaviour
{
    private Coroutine stayCoroutine = null;
    public float PushTime = 0.5f;

    public DoorController door;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && stayCoroutine == null)
        {
            stayCoroutine = StartCoroutine(StayTimer());
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && stayCoroutine != null)
        {
            StopCoroutine(stayCoroutine);
            stayCoroutine = null;
        }
    }

    private IEnumerator StayTimer()
    {
        yield return new WaitForSeconds(PushTime);

        door.isOpen = true;
    }
}
