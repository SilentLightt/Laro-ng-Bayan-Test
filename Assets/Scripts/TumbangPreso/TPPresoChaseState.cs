using System.Linq;
using UnityEngine;

public class TPPresoChaseState : ITPPresoState
{
    private readonly TPPresoAI presoAI;
    private Transform target;

    public TPPresoChaseState(TPPresoAI ai)
    {
        presoAI = ai;
    }

    public void EnterState()
    {
        Debug.Log("Preso AI: Entering Chase State");
        target = FindClosestAttacker();
    }

    public void UpdateState()
    {
        if (target == null || !TPCanManager.Instance.IsCanKnockedOver)
        {
            presoAI.ChangeState(new TPPresoIdleState(presoAI));
            return;
        }

        presoAI.transform.position = Vector3.MoveTowards(
            presoAI.transform.position,
            target.position,
            Time.deltaTime * 3f // Adjust speed as needed
        );

        float distance = Vector3.Distance(presoAI.transform.position, target.position);
        if (distance < 1.5f)
        {
            presoAI.ChangeState(new TPPresoTagState(presoAI, target));
        }
    }

    public void ExitState()
    {
        Debug.Log("Preso AI: Exiting Chase State");
    }

    private Transform FindClosestAttacker()
    {
        Collider[] hits = Physics.OverlapSphere(presoAI.transform.position, presoAI.detectionRadius, presoAI.attackerMask);
        if (hits.Length == 0) return null;

        return hits.OrderBy(h => Vector3.Distance(presoAI.transform.position, h.transform.position)).First().transform;
    }
}
