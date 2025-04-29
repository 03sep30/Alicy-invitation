using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public List<Transform> PortalList;

    public Transform EnterPortal()
    {
        int randomNum = Random.Range(0, PortalList.Count);
        Transform randomPortal = PortalList[randomNum];

        return randomPortal;
    }
}
