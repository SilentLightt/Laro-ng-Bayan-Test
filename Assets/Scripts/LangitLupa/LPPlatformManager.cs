using System.Collections.Generic;
using UnityEngine;
using System.Collections;
public class LPPlatformManager : MonoBehaviour
{
    public List<GameObject> platforms;
    public Transform[] spawnPoints; // New: Possible respawn locations
    private float minDisappearTime = 3f;
    private float maxDisappearTime = 7f;

    private void Start()
    {
        StartCoroutine(PlatformDisappearanceCycle());
    }

    private IEnumerator PlatformDisappearanceCycle()
    {
        while (true)
        {
            yield return new WaitForSeconds(UnityEngine.Random.Range(minDisappearTime, maxDisappearTime));
            RemoveAndRespawnPlatform();
        }
    }

    private void RemoveAndRespawnPlatform()
    {
        if (platforms.Count == 0) return;

        int index = UnityEngine.Random.Range(0, platforms.Count);
        GameObject platform = platforms[index];
        StartCoroutine(ShakeAndDestroy(platform, index));
    }

    private IEnumerator ShakeAndDestroy(GameObject platform, int index)
    {
        Vector3 originalPosition = platform.transform.position;
        float shakeDuration = 1f;
        float elapsed = 0f;

        while (elapsed < shakeDuration)
        {
            platform.transform.position = originalPosition + (Vector3.up * Mathf.Sin(Time.time * 50) * 0.1f);
            elapsed += Time.deltaTime;
            yield return null;
        }

        Destroy(platform);
        platforms.RemoveAt(index);

        yield return new WaitForSeconds(2f); // Delay before respawning

        RespawnPlatform(index);
    }

    private void RespawnPlatform(int index)
    {
        if (spawnPoints.Length == 0) return;

        Transform spawnPoint = spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Length)];
        GameObject newPlatform = Instantiate(platforms[0], spawnPoint.position, Quaternion.identity); // Using first platform as a template
        platforms.Add(newPlatform);
    }
}
