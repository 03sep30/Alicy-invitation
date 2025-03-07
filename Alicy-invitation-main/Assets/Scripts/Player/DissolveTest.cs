using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveTest : MonoBehaviour
{
    public float dissolveSpeed = 1f; // ������� �ӵ�
    private Material material;
    private float dissolveAmount = 0f;

    void Start()
    {
        material = GetComponent<Renderer>().material;
        material.SetFloat("_DissolveAmount", 0f); // ���̴��� ������ �� �ʱ�ȭ
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
            Destroy(gameObject); // ������ ������� ������Ʈ ����
        }
    }

}
