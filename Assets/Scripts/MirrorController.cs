using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorController : MonoBehaviour
{
    [Header("Small 0, Big 1, Normal 2")]
    public int MirrorState; 
    public float waitTime = 2f;

    private bool isWaiting = false;

    public void OnCollisionEnter(Collision collision)
    {
        var coll = gameObject.GetComponent<MeshCollider>();

        if ((int)GameManager.Instance.currentSize == MirrorState || GameManager.Instance.currentSize == CharacterSize.Normal)
        {
            coll.isTrigger = true;
        }
        else
        {
            coll.isTrigger = false;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<PlayerController>();
        if (player == null || isWaiting) return;

        if (MirrorState == 0)
        {
            GameManager.Instance.currentSize = CharacterSize.Small;
            player.UpdateStatus(0);
            StartWaiting();
        }
        if (MirrorState == 1)
        {
            GameManager.Instance.currentSize = CharacterSize.Big;
            player.UpdateStatus(1);
            StartWaiting();
        }
        if (MirrorState == 2)
        {
            GameManager.Instance.currentSize = CharacterSize.Normal;
            player.UpdateStatus(10);
            StartWaiting();
        }
    }

    private void StartWaiting()
    {
        isWaiting = true;
        StartCoroutine(WaitRoutine());
    }

    private IEnumerator WaitRoutine()
    {
        yield return new WaitForSeconds(waitTime);
        isWaiting = false;
    }
}
