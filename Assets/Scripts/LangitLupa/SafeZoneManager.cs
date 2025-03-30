using System.Collections.Generic;
using UnityEngine;

public class SafeZoneManager : MonoBehaviour
{
    public List<Transform> langitZones;
    private Transform currentLangit;
    private int previousIndex = -1;

    void Start()
    {
        InvokeRepeating(nameof(ChangeLangitZone), 0f, 5f);
    }

    void ChangeLangitZone()
    {
        if (langitZones.Count == 0) return;

        int newIndex;
        do { newIndex = Random.Range(0, langitZones.Count); } while (newIndex == previousIndex);

        previousIndex = newIndex;
        currentLangit = langitZones[newIndex];

        Debug.Log($"New Langit Zone: {currentLangit.name}");
        // Apply visual effect or notify players
    }

    public bool IsInSafeZone(Transform player)
    {
        if (currentLangit == null) return false;
        return Vector3.Distance(player.position, currentLangit.position) < 2f;
    }

}
