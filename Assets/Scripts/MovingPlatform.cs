using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Vector3 DeltaPosition { get; private set; }

    private Vector3 _lastPosition;

    void LateUpdate()
    {
        DeltaPosition = transform.position - _lastPosition;
        _lastPosition = transform.position;
    }
}