using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public List<Transform> PortalList;
    public bool isActive = false;
    public GameObject activeObj;

    public Transform EnterPortal()
    {
        int randomNum = Random.Range(0, PortalList.Count);
        Transform randomPortal = PortalList[randomNum];

        if (isActive)
        {
            activeObj.SetActive(true);
            var text = activeObj.GetComponent<TextController>();
            if (text != null)
            {
                text.StartTextDisplay();
            }
        }

        return randomPortal;
    }
}
