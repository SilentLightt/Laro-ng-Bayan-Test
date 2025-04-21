using UnityEngine;

public class TPTaggingZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TPTaggingUI.Instance.ShowSafeZone(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TPTaggingUI.Instance.ShowSafeZone(true);
        }
    }
}
