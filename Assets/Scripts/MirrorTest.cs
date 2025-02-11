using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorTest : MonoBehaviour
{
    public Transform player;
    public Transform mirror;

    private void Update()
    {
        Vector3 localPlayer = mirror.InverseTransformPoint(player.position);
        transform.position = mirror.TransformPoint(new Vector3(localPlayer.x, localPlayer.y, -localPlayer.z));


        Vector3 localMirror = mirror.TransformPoint(new Vector3(-localPlayer.x, localPlayer.y, localPlayer.z));
        transform.LookAt(localMirror);
    }
}