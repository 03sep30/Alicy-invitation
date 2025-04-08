using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Mushroom : MonoBehaviour
{
    public abstract void ChangeState(PlayerController player);
}
