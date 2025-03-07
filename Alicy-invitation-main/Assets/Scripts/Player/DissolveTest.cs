using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveTest : MonoBehaviour
{
    public float dissolveSpeed = 1f; // 사라지는 속도
    private Material material;
    private float dissolveAmount = 0f;

    void Start()
    {
        material = GetComponent<Renderer>().material;
        material.SetFloat("_DissolveAmount", 0f); // 쉐이더에 전달할 값 초기화
    }

    void Update()
    {
        if (dissolveAmount < 1f)
        {
            dissolveAmount += Time.deltaTime * dissolveSpeed;
            material.SetFloat("_DissolveAmount", dissolveAmount);
        }
        else
        {
            Destroy(gameObject); // 완전히 사라지면 오브젝트 제거
        }
    }

}
