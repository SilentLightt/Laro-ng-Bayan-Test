using UnityEngine;
using System.Collections;
public class TPPresoIdleState : ITPPresoState
{
    private readonly TPPresoAI presoAI;

    public TPPresoIdleState(TPPresoAI ai)
    {
        presoAI = ai;
    }

    public void EnterState()
    {
        Debug.Log("Preso AI: Entering Idle State");
    }

    public void UpdateState()
    {
        if (TPCanManager.Instance.IsCanKnockedOver &&
            Physics.CheckSphere(presoAI.transform.position, presoAI.detectionRadius, presoAI.attackerMask))
        {
            presoAI.ChangeState(new TPPresoChaseState(presoAI));
        }
    }

    public void ExitState()
    {
        Debug.Log("Preso AI: Exiting Idle State");
    }
}
