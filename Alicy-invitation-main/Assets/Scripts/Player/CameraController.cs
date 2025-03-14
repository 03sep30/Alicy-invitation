using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public Transform Point;
    public float distance = 5f;
    public float rotateSpeed = 5f;
    public float zoomSpeed = 2f;
    public float minDistance = 2f;
    public float maxDistance = 20f;

    private PlayerController playerController;

    private float yaw = 0f;
    private float pitch = 10f;

    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();

        UpdateCameraPosition();
    }

    void Update()
    {
        if (playerController.currentSize == CharacterSize.Normal)
        {
            minDistance = 2f;
            maxDistance = 30f;
        }
        if (playerController.currentSize == CharacterSize.Small)
        {
            minDistance = 6.5f;
            maxDistance = 15f;
        }
        if (playerController.currentSize == CharacterSize.Big)
        {
            minDistance = 30f;
            maxDistance = 45f;
        }

        if (distance > maxDistance)
        {
            distance = maxDistance;
        }

        yaw += Input.GetAxis("Mouse X") * rotateSpeed;
        pitch -= Input.GetAxis("Mouse Y") * rotateSpeed;
        pitch = Mathf.Clamp(pitch, -30f, 60f);

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0f)
        {
            distance -= scroll * zoomSpeed;
            distance = Mathf.Clamp(distance, minDistance, maxDistance);
        }

        UpdateCameraPosition();
    }

    void UpdateCameraPosition()
    {
        player.rotation = Quaternion.Euler(0, yaw, 0);

        Vector3 direction = new Vector3(0, 8, -distance);
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);
        transform.position = player.position + rotation * direction;

        transform.LookAt(Point.position);
    }

    public Vector3 GetCameraForward()
    {
        return Vector3.Scale(transform.forward, new Vector3(1, 0, 1)).normalized;
    }
}