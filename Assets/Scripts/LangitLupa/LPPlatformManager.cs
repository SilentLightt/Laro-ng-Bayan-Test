using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class LPPlatformManager : MonoBehaviour
{
    public float platformDisappearInterval = 7f;
    public float platformRespawnDelay = 5f;
    public GameObject platformPrefab;
    public List<Transform> spawnPoints;

    private Dictionary<Transform, LPPlatform> activePlatforms = new Dictionary<Transform, LPPlatform>();

    private void Start()
    {
        SpawnPlatforms();
        StartCoroutine(PlatformDisappearanceCycle());
    }

    private void SpawnPlatforms()
    {
        foreach (Transform spawnPoint in spawnPoints)
        {
            SpawnPlatformAt(spawnPoint);
        }
    }

    private void SpawnPlatformAt(Transform spawnPoint)
    {
        if (!activePlatforms.ContainsKey(spawnPoint))
        {
            GameObject newPlatform = Instantiate(platformPrefab, spawnPoint.position, Quaternion.identity);
            LPPlatform platformScript = newPlatform.GetComponent<LPPlatform>();
            if (platformScript != null)
            {
                activePlatforms[spawnPoint] = platformScript;
            }
        }
    }

    private IEnumerator PlatformDisappearanceCycle()
    {
        while (true) // Keep the cycle running indefinitely
        {
            yield return new WaitForSeconds(platformDisappearInterval);

            Transform spawnPoint = GetRandomPlatformSpawnPoint();
            if (spawnPoint != null && activePlatforms.ContainsKey(spawnPoint))
            {
                LPPlatform platformToRemove = activePlatforms[spawnPoint];
                platformToRemove.WarnBeforeDisappearing();
                yield return new WaitForSeconds(2f);
                platformToRemove.Disappear();
                activePlatforms.Remove(spawnPoint);

                // Respawn after delay
                StartCoroutine(RespawnPlatform(spawnPoint));
            }
        }
    }

    private IEnumerator RespawnPlatform(Transform spawnPoint)
    {
        yield return new WaitForSeconds(platformRespawnDelay);
        SpawnPlatformAt(spawnPoint);
    }

    private Transform GetRandomPlatformSpawnPoint()
    {
        if (activePlatforms.Count == 0) return null;
        List<Transform> keys = activePlatforms.Keys.ToList();
        return keys[Random.Range(0, keys.Count)];
    }
}





//using System.Collections.Generic;
//using UnityEngine;
//using System.Collections;
//public class LPPlatformManager : MonoBehaviour
//{
//    public List<GameObject> platforms;
//    public Transform[] spawnPoints; // New: Possible respawn locations
//    private float minDisappearTime = 3f;
//    private float maxDisappearTime = 7f;

//    private void Start()
//    {
//        StartCoroutine(PlatformDisappearanceCycle());
//    }

//    private IEnumerator PlatformDisappearanceCycle()
//    {
//        while (true)
//        {
//            yield return new WaitForSeconds(UnityEngine.Random.Range(minDisappearTime, maxDisappearTime));
//            RemoveAndRespawnPlatform();
//        }
//    }

//    private void RemoveAndRespawnPlatform()
//    {
//        if (platforms.Count == 0) return;

//        int index = UnityEngine.Random.Range(0, platforms.Count);
//        GameObject platform = platforms[index];
//        StartCoroutine(ShakeAndDestroy(platform, index));
//    }

//    private IEnumerator ShakeAndDestroy(GameObject platform, int index)
//    {
//        Vector3 originalPosition = platform.transform.position;
//        float shakeDuration = 1f;
//        float elapsed = 0f;

//        while (elapsed < shakeDuration)
//        {
//            platform.transform.position = originalPosition + (Vector3.up * Mathf.Sin(Time.time * 50) * 0.1f);
//            elapsed += Time.deltaTime;
//            yield return null;
//        }

//        Destroy(platform);
//        platforms.RemoveAt(index);

//        yield return new WaitForSeconds(2f); // Delay before respawning

//        RespawnPlatform(index);
//    }

//    private void RespawnPlatform(int index)
//    {
//        if (spawnPoints.Length == 0) return;

//        Transform spawnPoint = spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Length)];
//        GameObject newPlatform = Instantiate(platforms[0], spawnPoint.position, Quaternion.identity); // Using first platform as a template
//        platforms.Add(newPlatform);
//    }
//}
