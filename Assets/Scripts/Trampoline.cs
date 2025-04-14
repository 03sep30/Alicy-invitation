using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour
{
    private ThirdPersonController thirdPersonController;
    public float TrampolineForce;

    void Start()
    {
        thirdPersonController = FindObjectOfType<ThirdPersonController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        //if (other.CompareTag("Player"))
        //{
        //    Debug.Log("trampoline");
        //    thirdPersonController._rigidbody.AddForce(Vector3.up * TrampolineForce, ForceMode.Impulse);
        //}
    }
}
