using UnityEngine;

public class PlayerFootprint : MonoBehaviour
{
    [SerializeField] private GameObject footprintPrefab;
    [SerializeField] private float footprintLifetime = 3f;
    [SerializeField] private Vector3 footprintOffset = new Vector3(0, 0.01f, 0);

    public void CreateFootprint(Transform footTransform, bool isLeftFoot)
    {
        Vector3 spawnPos = footTransform.position + footprintOffset;
        Quaternion spawnRot = Quaternion.Euler(0, transform.eulerAngles.y, 0);

        GameObject footprint = Instantiate(footprintPrefab, spawnPos, spawnRot);

        if (!isLeftFoot)
        {
            footprint.transform.Rotate(0, 0, 180);
        }

        Destroy(footprint, footprintLifetime);
    }
}
