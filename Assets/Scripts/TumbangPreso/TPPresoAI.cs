using UnityEngine;

public class TPPresoAI : MonoBehaviour
{
    private ITPPresoState currentState;
    public TPTaggingZone TagZone;
    public float detectionRadius = 10f;
    public LayerMask attackerMask;

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
}
