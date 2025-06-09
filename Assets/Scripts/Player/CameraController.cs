using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public Transform Point;
    public float distance = 5f;
    public float minDistance = 2f;
    public float maxDistance = 20f;

    void Start()
    {
        UpdateCameraPosition();
    }

    void Update()
    {

        if (distance > maxDistance)
        {
            distance = maxDistance;
        }

        UpdateCameraPosition();
    }

    void UpdateCameraPosition()
    {
        Vector3 direction = new Vector3(0, 8, -distance);
        transform.position = player.position + direction;
        transform.LookAt(Point.position);
    }
}