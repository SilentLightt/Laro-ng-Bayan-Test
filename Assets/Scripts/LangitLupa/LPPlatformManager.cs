using System.Collections.Generic;
using UnityEngine;
using System.Collections;
public class LPPlatformManager : MonoBehaviour
{
    public List<GameObject> platforms;
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
            RemoveRandomPlatform();
        }
    }

    private void RemoveRandomPlatform()
    {
        if (platforms.Count == 0) return;

        int index = UnityEngine.Random.Range(0, platforms.Count);
        GameObject platform = platforms[index];
        StartCoroutine(ShakeAndDestroy(platform));
        platforms.RemoveAt(index);
    }

    private IEnumerator ShakeAndDestroy(GameObject platform)
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
    }
}