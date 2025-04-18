using UnityEngine;

public class SlipperPickupZone : MonoBehaviour
{
    private bool canPickup = false;
    private GameObject player;
    private TPManager tpmanager;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canPickup = true;
            player = other.gameObject;
            Debug.Log("Player can pick up slipper.");
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (canPickup && Input.GetKeyDown(KeyCode.E))
        {
            PickupSlipper();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canPickup = false;
            player = null;
        }
    }

    void PickupSlipper()
    {
        // You could also just destroy the slipper and increment a slipper count
        Destroy(transform.parent.gameObject); // Destroys the slipper prefab
        tpmanager.OnSlipperRetrieved(); // Notify GameManager
    }
}
