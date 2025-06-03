using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    public void StartDestroy(GameObject obj, float time)
    {
        StartCoroutine(DestroyObjectCoroutine(obj, time));
    }

    IEnumerator DestroyObjectCoroutine(GameObject obj, float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(obj);
    }
}
