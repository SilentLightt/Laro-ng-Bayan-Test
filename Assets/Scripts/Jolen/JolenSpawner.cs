using UnityEngine;

public class JolenSpawner : MonoBehaviour, IJolenSpawner
{
    [SerializeField] private GameObject jolenPrefab;
    [SerializeField] private Transform squareArea;
    [SerializeField] private int jolenCount = 5;

    public void Spawn()
    {
        if (squareArea == null || jolenPrefab == null) return;

        Vector3 areaSize = squareArea.localScale;
        Vector3 areaCenter = squareArea.position;

        for (int i = 0; i < jolenCount; i++)
        {
            Vector3 pos = new Vector3(
                Random.Range(areaCenter.x - areaSize.x / 2, areaCenter.x + areaSize.x / 2),
                areaCenter.y,
                Random.Range(areaCenter.z - areaSize.z / 2, areaCenter.z + areaSize.z / 2)
            );

            Instantiate(jolenPrefab, pos, Quaternion.identity);
        }
    }
}
