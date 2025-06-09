using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public Transform Point;
    public Vector3 direction;

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
        transform.position = player.position + direction;
        //transform.position = new Vector3(direction.x, direction.y, player.position.z);
        transform.LookAt(Point.position);
    }
}