using System.Collections;
using UnityEngine;

public class DiagonalPlatform : MonoBehaviour
{
    public Transform[] waypoints;
    public float moveSpeed = 2f;
    public float waitTimeAtPoint = 2f;

    public int currentWaypointIndex = 0;
    private bool isWaiting = false;
    private bool goingForward = true;  // true = 0-1-2, false = 2-1-0

    void Start()
    {
        if (waypoints.Length == 0)
        {
            Debug.LogError("Waypoints가 설정되지 않았습니다!");
            enabled = false;
        }
    }

    void FixedUpdate()
    {
        if (isWaiting || waypoints.Length == 0) return;

        Transform target = waypoints[currentWaypointIndex];
        transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.fixedDeltaTime);

        if (Vector3.Distance(transform.position, target.position) < 0.01f)
        {
            StartCoroutine(WaitAtWaypoint());
        }
    }

    IEnumerator WaitAtWaypoint()
    {
        isWaiting = true;
        yield return new WaitForSeconds(waitTimeAtPoint);

        // 방향 설정
        if (goingForward)
        {
            currentWaypointIndex++;
            if (currentWaypointIndex >= waypoints.Length)
            {
                currentWaypointIndex = waypoints.Length - 2;
                goingForward = false;
            }
        }
        else
        {
            currentWaypointIndex--;
            if (currentWaypointIndex < 0)
            {
                currentWaypointIndex = 1;
                goingForward = true;
            }
        }

        isWaiting = false;
    }
}
