using UnityEngine;
using System.Collections;

public class LPPlatform : MonoBehaviour
{
    private Vector3 originalPosition;
    private AudioSource audioSource;
    public AudioClip warningClip;

    private void Start()
    {
        originalPosition = transform.position;
        audioSource = GetComponent<AudioSource>();
    }

    public void WarnBeforeDisappearing()
    {
        if (audioSource && warningClip)
            audioSource.PlayOneShot(warningClip);
        StartCoroutine(ShakeEffect());
    }

    private IEnumerator ShakeEffect()
    {
        float duration = 2f;
        float elapsed = 0f;
        while (elapsed < duration)
        {
            transform.position = originalPosition + (Vector3.right * Mathf.Sin(Time.time * 50) * 0.1f);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.position = originalPosition;
    }

    public void Disappear()
    {
        gameObject.SetActive(false);
        Destroy(gameObject, 1f); // Destroy the object to allow respawn
    }
}
