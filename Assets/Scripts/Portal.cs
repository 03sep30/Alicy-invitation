using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public List<Transform> PortalList;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            int randomNum = Random.Range(0, PortalList.Count);
            Transform randomPortal = PortalList[randomNum];
            other.transform.position = randomPortal.position;
        }
    }
}
