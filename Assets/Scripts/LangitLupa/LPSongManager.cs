using UnityEngine;
using System;
using System.Collections;   
public class LPSongManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip chantClip;
    public event Action OnChantTrigger;

    private void Start()
    {
        StartCoroutine(ChantRoutine());
    }

    private IEnumerator ChantRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(UnityEngine.Random.Range(3f, 7f));
            PlayChant();
            OnChantTrigger?.Invoke();
        }
    }

    private void PlayChant()
    {
        if (audioSource && chantClip)
        {
            audioSource.PlayOneShot(chantClip);
        }
    }
}