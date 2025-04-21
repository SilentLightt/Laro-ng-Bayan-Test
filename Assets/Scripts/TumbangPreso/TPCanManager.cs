using UnityEngine;

public class TPCanManager : MonoBehaviour
{
    public static TPCanManager Instance { get; private set; }

    [HideInInspector] public bool IsCanKnockedOver = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    // Call this from your can physics when the can falls
    public void SetCanKnockedState(bool isKnocked)
    {
        IsCanKnockedOver = isKnocked;
    }
}
