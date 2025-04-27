using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pot : MonoBehaviour
{
    [SerializeField]
    private float potTime;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 7)
        {
            GetComponent<Rigidbody>().mass = 1000f;
            StartCoroutine(PotTime());
        }
    }

    private IEnumerator PotTime()
    {
        yield return new WaitForSeconds(potTime);

        Destroy(gameObject);
    }
}
