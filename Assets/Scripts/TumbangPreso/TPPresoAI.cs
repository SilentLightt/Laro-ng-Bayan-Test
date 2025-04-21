using UnityEngine;

public class TPPresoAI : MonoBehaviour
{
    private ITPPresoState currentState;

    [Header("Detection Settings")]
    public float detectionRadius = 10f;
    public float tagDistance = 2f;
    public LayerMask attackerMask;
    public Collider taggingZone; // Assign this in the inspector or dynamically

    private void Start()
    {
        ChangeState(new TPPresoIdleState(this));
    }

    private void Update()
    {
        currentState?.UpdateState();
    }

    public void ChangeState(ITPPresoState newState)
    {
        currentState?.ExitState();
        currentState = newState;
        currentState.EnterState();
    }

    public bool IsWithinTagZone(Vector3 attackerPos)
    {
        return taggingZone.bounds.Contains(attackerPos);
    }
}
