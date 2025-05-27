using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public GameObject leftObj;
    public GameObject rightObj;

    public Vector3 closedLeftRot;
    public Vector3 openLeftRot;
    public Vector3 closedRightRot;
    public Vector3 openRightRot;

    public float rotateSpeed = 2f;

    public bool isOpen = false;

    void Update()
    {
        if (leftObj != null)
        {
            Quaternion targetRot = Quaternion.Euler(isOpen ? openLeftRot : closedLeftRot);
            leftObj.transform.localRotation = Quaternion.Slerp(leftObj.transform.localRotation, targetRot, rotateSpeed * Time.deltaTime);
        }

        if (rightObj != null)
        {
            Quaternion targetRot = Quaternion.Euler(isOpen ? openRightRot : closedRightRot);
            rightObj.transform.localRotation = Quaternion.Slerp(rightObj.transform.localRotation, targetRot, rotateSpeed * Time.deltaTime);
        }
    }
}
