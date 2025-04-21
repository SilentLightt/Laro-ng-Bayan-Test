using UnityEngine;

public class TPTaggingUI : MonoBehaviour
{
    public static TPTaggingUI Instance { get; private set; }
    [SerializeField] private GameObject safeZonePopup;

    private void Awake()
    {
        Instance = this;
        safeZonePopup.SetActive(false);
    }

    public void ShowSafeZone(bool isSafe)
    {
        safeZonePopup.SetActive(isSafe);
    }
}
