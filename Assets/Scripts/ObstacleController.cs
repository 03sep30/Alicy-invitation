using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    public GameObject leftObj;
    public GameObject rightObj;

    public Vector3 leftRot;
    public Vector3 rightRot;
    public Vector3 leftPos;
    public Vector3 rightPos;

    public float moveSpeed = 1f;
    public float rotateSpeed = 180f;

    void Update()
    {
        if (leftObj != null)
        {
            leftObj.transform.localPosition = Vector3.MoveTowards(leftObj.transform.localPosition, leftPos, moveSpeed * Time.deltaTime);

            Quaternion targetLeftRot = Quaternion.Euler(leftRot);
            leftObj.transform.localRotation = Quaternion.RotateTowards(leftObj.transform.localRotation, targetLeftRot, rotateSpeed * Time.deltaTime);
        }

        if (rightObj != null)
        {
            rightObj.transform.localPosition = Vector3.MoveTowards(rightObj.transform.localPosition, rightPos, moveSpeed * Time.deltaTime);

            Quaternion targetRightRot = Quaternion.Euler(rightRot);
            rightObj.transform.localRotation = Quaternion.RotateTowards(rightObj.transform.localRotation, targetRightRot, rotateSpeed * Time.deltaTime);
        }
    }
}
