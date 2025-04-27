using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootCollider : MonoBehaviour
{
    [SerializeField] private PlayerFootprint footprintCreator;
    [SerializeField] private bool isLeftFoot;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7) 
        {
            footprintCreator.CreateFootprint(transform, isLeftFoot);
        }
    }
}
