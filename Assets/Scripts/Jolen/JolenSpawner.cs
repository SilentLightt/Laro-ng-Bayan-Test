using UnityEngine;

public class JolenSpawner : MonoBehaviour, IJolenSpawner
{
    [SerializeField] private GameObject jolenPrefab;
    [SerializeField] private Transform circleArea;
    [SerializeField] private int jolenCount = 15;
    [SerializeField] private float radius = 8f; 

    public void Spawn()
    {
        if (circleArea == null || jolenPrefab == null) return;

        Vector3 areaCenter = circleArea.position;

        float angleStep = 2f * Mathf.PI / jolenCount;

        for (int i = 0; i < jolenCount; i++)
        {
            float angle = i * angleStep;

            float x = areaCenter.x + Mathf.Cos(angle) * radius;
            float z = areaCenter.z + Mathf.Sin(angle) * radius;

            Vector3 pos = new Vector3(x, areaCenter.y, z);

            Instantiate(jolenPrefab, pos, Quaternion.identity);
        }
    }

    private void OnDrawGizmos()
    {
        if (circleArea != null)
        {
            Gizmos.color = new Color(1f, 0f, 0f, 0.3f);

            Gizmos.DrawWireSphere(circleArea.position, radius);
        }
    }
}
