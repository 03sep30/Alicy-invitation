using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour
{
    private ThirdPersonController thirdPersonController;
    public float TrampolineForce;
    public bool isLimitedUse = false;
    public bool isLimited = false;

    void Start()
    {
        thirdPersonController = FindObjectOfType<ThirdPersonController>();
    }
}
