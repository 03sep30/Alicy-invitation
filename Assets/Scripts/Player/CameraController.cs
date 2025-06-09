using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public Vector3 direction;

    public float speed = 5f;
    public float minDistance = 2f;
    public float maxDistance = 20f;
    public float distance = 5f;

    public float upwardFollowSpeed = 2f;
    public float downwardFollowSpeed = 5f;
    public float elevationThreshold = 1.5f;
    public float elevationTimeThreshold = 0.3f;

    public float baseY;
    public float currentY;
    private float elevationTimer = 0f;
    private bool shouldFollowY = false;

    private PlayerController playerController;

    void Start()
    {
        baseY = player.position.y;
        currentY = direction.y;
        playerController = player.GetComponent<PlayerController>();
        UpdateCameraPosition();
    }
    void Update()
    {
        distance = Mathf.Clamp(distance, minDistance, maxDistance);

        bool isStepActive = (playerController != null && playerController.isOnStep);

        if (isStepActive)
        {
            float targetY = player.position.y;
            float heightDiff = targetY - baseY;

            if (heightDiff > elevationThreshold)
            {
                elevationTimer += Time.deltaTime;

                if (elevationTimer >= elevationTimeThreshold)
                {
                    shouldFollowY = true;
                }
            }
            else
            {
                elevationTimer = 0f;
                shouldFollowY = false;
            }

            // Y 오프셋 계산
            if (shouldFollowY)
            {
                currentY = Mathf.Lerp(currentY, player.position.y - baseY + direction.y, Time.deltaTime * upwardFollowSpeed);
            }
            else
            {
                currentY = Mathf.Lerp(currentY, direction.y, Time.deltaTime * downwardFollowSpeed);
            }
        }
        // else 블록 없음 → 점프 중엔 currentY 유지

        UpdateCameraPosition();
    }



    void UpdateCameraPosition()
    {
        Vector3 cameraOffset = new Vector3(direction.x, currentY, direction.z);
        Vector3 targetPosition = player.position + cameraOffset;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // 필요시 주석 해제해서 카메라가 플레이어를 보게 할 수 있음
        // transform.LookAt(player);
    }
}
