using UnityEngine;

public class PlayerFootprint : MonoBehaviour
{
    [SerializeField] private GameObject leftFootprintPrefab;
    [SerializeField] private GameObject rightFootprintPrefab;
    [SerializeField] private float footprintLifetime = 3f;
    [SerializeField] private Vector3 footprintOffset = new Vector3(0, 0.01f, 0);

    public void CreateFootprint(Transform footTransform, bool isLeftFoot)
    {
        Vector3 spawnPos = footTransform.position + footprintOffset;
        Quaternion spawnRot = Quaternion.Euler(0, transform.eulerAngles.y, 0);

        if (!isLeftFoot)
        {
            GameObject rightFootprint = Instantiate(rightFootprintPrefab, spawnPos, spawnRot);
            rightFootprint.transform.Rotate(0, 180, 0);
            Destroy(rightFootprint, footprintLifetime);
        }
        else
        {
            GameObject leftFootprint = Instantiate(leftFootprintPrefab, spawnPos, spawnRot);
            leftFootprint.transform.Rotate(0, 180, 0);
            Destroy(leftFootprint, footprintLifetime);
        }
    }
}
