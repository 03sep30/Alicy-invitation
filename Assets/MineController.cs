using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineController : MonoBehaviour
{
    public Transform target;

    [Header("Ghost")]
    public GameObject ghostPrefab;
    public Transform throwPoint;
    public float ghostSpeed;
    public bool ghostMine = false;

    [Header("Puzzle")]
    public GameObject puzzle;
    public bool activePuzzle = false;

    [Header("Heal")]
    public bool heal = false;

    [Header("Key")]
    public bool key = false;

    void Start()
    {
        target = FindObjectOfType<PlayerController>().gameObject.transform;   
    }

    void Update()
    {
        if (!ghostMine) return;

        Vector3 targetPos = target.position;

        Vector3 direction = (targetPos - throwPoint.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        throwPoint.rotation = Quaternion.Lerp(throwPoint.rotation, targetRotation, Time.deltaTime * 5f);
    }

    public void GhostRun()
    {
        if (!ghostMine) return;

        if (target != null)
        {
            GameObject lollipop = Instantiate(ghostPrefab, throwPoint.position, Quaternion.identity);
            Rigidbody rb = lollipop.GetComponent<Rigidbody>();

            Vector3 targetPos = target.position;
            Vector3 direction = (targetPos - throwPoint.position).normalized;

            rb.AddForce(direction * ghostSpeed);
        }
    }

    public void GetPuzzle()
    {
        if (!activePuzzle) return;
        puzzle.SetActive(true);
        Debug.Log("���� ȹ��");
    }

    public void MinePlayerHeal()
    {
        if (!heal) return;
        PlayerHealth playerHealth = target.GetComponentInChildren<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.PlayerHeal(1);
            Debug.Log("1 ȸ��");
        }
    }

    public void MineGetKey()
    {
        if (!key) return;
        PlayerController playerController = target.GetComponent<PlayerController>();
        playerController.GetKey();
        Debug.Log("���� ȹ��");
    }
}
