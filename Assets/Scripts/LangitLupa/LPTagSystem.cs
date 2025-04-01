using UnityEngine;

public class LPTagSystem : MonoBehaviour
{
    public float tagRange = 2f;
    public KeyCode tagKey = KeyCode.E;

    private LPPlayerRole playerRole;

    private void Start()
    {
        playerRole = GetComponent<LPPlayerRole>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(tagKey) && playerRole.CanTag())
        {
            AttemptTag();
        }
    }

    private void AttemptTag()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, tagRange);
        foreach (Collider hit in hitColliders)
        {
            LPPlayerRole otherPlayer = hit.GetComponent<LPPlayerRole>();
            if (otherPlayer != null && otherPlayer.CurrentRole == LPPlayerRole.Role.Runner)
            {
                TransferTagging(otherPlayer);
                break;
            }
        }
    }
    private void TransferTagging(LPPlayerRole newTaya)
    {
        // Remove LPTagSystem from current Taya
        Destroy(this);

        // Swap roles
        newTaya.BecomeTaya();
        playerRole.BecomeRunner();

        // Add LPTagSystem to the new Taya
        newTaya.gameObject.AddComponent<LPTagSystem>();
    }
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, tagRange);
    }
}
