using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorController : MonoBehaviour
{
    [Header("작아지는건 0, 커지는건 1")]
    public int MirrorState; 
    public float waitTime = 2f;

    private bool isWaiting = false;

    public void OnCollisionEnter(Collision collision)
    {
        var coll = gameObject.GetComponent<MeshCollider>();
        var player = collision.gameObject.GetComponent<PlayerController>();

        if ((int)player.currentSize == MirrorState || player.currentSize == CharacterSize.Normal)
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

        if (player.currentSize == CharacterSize.Normal)
        {
            if (MirrorState == 0)
            {
                player.gameObject.transform.localScale = new Vector3(2.5f, 2.5f, 2.5f);
                player.currentSize = CharacterSize.Small;
                StartWaiting();
            }
            if (MirrorState == 1)
            {
                player.gameObject.transform.localScale = new Vector3(10f, 10f, 10f);
                player.currentSize = CharacterSize.Big;
                StartWaiting();
            }
        }
        else
        {
            player.gameObject.transform.localScale = new Vector3(5f, 5f, 5f);
            player.currentSize = CharacterSize.Normal;
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
