using UnityEngine;
using System.Collections;
public enum PresoAIState
{
    Idle,
    Chase,
    Tag,
    Reset
}

public class PresoAIController : MonoBehaviour
{
    [SerializeField] private float detectionRadius = 10f;
    [SerializeField] private LayerMask attackerMask;
    [SerializeField] private TagZone tagZone;

    private PresoAIState currentState = PresoAIState.Idle;
    private GameObject targetAttacker;

    private void Update()
    {
        switch (currentState)
        {
            case PresoAIState.Idle:
                CheckForTarget();
                break;
            case PresoAIState.Chase:
                ChaseTarget();
                break;
            case PresoAIState.Tag:
                TryTag();
                break;
            case PresoAIState.Reset:
                ResetCan();
                break;
        }
    }

    private void CheckForTarget()
    {
        if (!CanManager.Instance.IsCanKnocked()) return;

        Collider[] attackers = Physics.OverlapSphere(transform.position, detectionRadius, attackerMask);

        foreach (var a in attackers)
        {
            if (tagZone.IsInsideZone(a.transform))
            {
                targetAttacker = a.gameObject;
                currentState = PresoAIState.Chase;
                break;
            }
        }
    }

    private void ChaseTarget()
    {
        if (targetAttacker == null)
        {
            currentState = PresoAIState.Idle;
            return;
        }

        // Move using NavMesh
        GetComponent<UnityEngine.AI.NavMeshAgent>().SetDestination(targetAttacker.transform.position);

        float dist = Vector3.Distance(transform.position, targetAttacker.transform.position);
        if (dist < 2f) currentState = PresoAIState.Tag;
    }

    private void TryTag()
    {
        // Use Raycast here for precise check
        if (Vector3.Distance(transform.position, targetAttacker.transform.position) <= 2f)
        {
            if (tagZone.IsInsideZone(targetAttacker.transform))
            {
                // Perform tag
                Debug.Log("Attacker Tagged!");
                currentState = PresoAIState.Idle;
            }
        }
    }

    private void ResetCan()
    {
        // If no attackers in zone for 3 seconds
        if (!CanManager.Instance.IsCanKnocked()) currentState = PresoAIState.Idle;
    }
}
