using UnityEngine;

public class LPTayaRestriction : MonoBehaviour
{
    private LPPlayerRole playerRole;
    private CharacterController characterController;
    public string langitTag = "Langit"; // Platforms with this tag are "Langit"
    public float detectionRange = 0.1f; // Slightly above ground to detect surface

    private void Start()
    {
        playerRole = FindFirstObjectByType<LPPlayerRole>();
        characterController = FindFirstObjectByType<CharacterController>();
    }

    private void Update()
    {
        if (playerRole.CurrentRole == LPPlayerRole.Role.Taya)
        {
            if (IsOnLangit())
            {
                Debug.Log("[LPTayaRestriction] Taya is ON a Langit platform! Restricting movement.");
                PreventMovement();
            }
            else
            {
                Debug.Log("[LPTayaRestriction] Taya is NOT on Langit.");
            }
        }
    }

    private bool IsOnLangit()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.up, out hit, detectionRange))
        {
            if (hit.collider.CompareTag(langitTag))
            {
                Debug.Log($"[LPTayaRestriction] Detected Langit: {hit.collider.name}");
                return true;
            }
        }
        return false;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 start = transform.position;
        Vector3 end = start + Vector3.up * detectionRange;
        Gizmos.DrawLine(start, end);
        Gizmos.DrawSphere(end, 0.05f);
    }
    private void PreventMovement()
    {
        if (characterController != null)
        {
            characterController.Move(Vector3.zero);
        }
    }
}
