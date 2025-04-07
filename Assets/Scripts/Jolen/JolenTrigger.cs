using UnityEngine;

public class JolenTrigger : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Jolen"))
        {
            // Handle point scoring logic here
            Debug.Log("Jolen exited the scoring zone!");
        }
    }
}
