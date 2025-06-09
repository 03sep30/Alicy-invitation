using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public Transform Point;
    public Vector3 direction;

    public float speed;
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
        Vector3 a = new Vector3(player.position.x + direction.x, direction.y, player.position.z + direction.z);
        //transform.position = player.position + direction;
        transform.position = Vector3.MoveTowards(transform.position, player.position + direction, speed);
        //transform.LookAt(Point.position);
    }
}