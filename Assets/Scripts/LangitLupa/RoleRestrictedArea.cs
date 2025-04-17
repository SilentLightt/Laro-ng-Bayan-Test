using UnityEngine;

[RequireComponent(typeof(Collider))]
public class RoleRestrictedArea : MonoBehaviour
{
    public LPPlayerRole.Role restrictedRole = LPPlayerRole.Role.Runner;
    public Vector3 pushBackDirection = Vector3.back;
    public float pushBackForce = 5f;
    private void OnTriggerStay(Collider other)
    {
        LPPlayerRole playerRole = other.GetComponent<LPPlayerRole>();
        Rigidbody playerRb = other.GetComponent<Rigidbody>();

        if (playerRole != null && playerRole.CurrentRole == restrictedRole && playerRb != null)
        {
            Debug.Log("Access Denied: " + restrictedRole + " cannot enter this area.");

            // Option 1: Push back
            //playerRb.AddForce(pushBackDirection.normalized * pushBackForce, ForceMode.Impulse);

            // Optional: Teleport them back to a safe position instead
            other.transform.position = new Vector3(57, 2, 42); // Replace with a safe position
        }
    }
    //    private void OnCollisionEnter(Collision other)
    //    {
    //        LPPlayerRole playerRole = other.GetComponent<LPPlayerRole>();
    //        Rigidbody playerRb = other.GetComponent<Rigidbody>();

    //        if (playerRole != null && playerRole.CurrentRole == restrictedRole && playerRb != null)
    //        {
    //            Debug.Log("Access Denied: " + restrictedRole + " cannot enter this area.");

    //            // Option 1: Push back
    //            playerRb.AddForce(pushBackDirection.normalized * pushBackForce, ForceMode.Impulse);

    //            // Optional: Teleport them back to a safe position instead
    //            // other.transform.position = new Vector3(0, 1, 0); // Replace with a safe position
    //        }
    //}
    private void OnTriggerEnter(Collider other)
    {
        LPPlayerRole playerRole = other.GetComponent<LPPlayerRole>();
        Rigidbody playerRb = other.GetComponent<Rigidbody>();

        if (playerRole != null && playerRole.CurrentRole == restrictedRole && playerRb != null)
        {
            Debug.Log("Access Denied: " + restrictedRole + " cannot enter this area.");

            // Option 1: Push back
          //  playerRb.AddForce(pushBackDirection.normalized * pushBackForce, ForceMode.Impulse);

            // Optional: Teleport them back to a safe position instead
            other.transform.position = new Vector3(57, 2, 42); // Replace with a safe position
        }
    }
}
