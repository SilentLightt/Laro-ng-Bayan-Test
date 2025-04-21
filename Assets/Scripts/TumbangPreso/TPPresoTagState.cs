using UnityEngine;

public class TPPresoTagState : ITPPresoState
{
    private readonly TPPresoAI presoAI;
    private readonly Transform target;

    private float tagCooldown = 2f;
    private float lastTagTime = -999f;

    public TPPresoTagState(TPPresoAI ai, Transform target)
    {
        this.presoAI = ai;
        this.target = target;
    }

    public void EnterState()
    {
        Debug.Log("Preso AI: Entering Tag State");
    }

    public void UpdateState()
    {
        if (!TPCanManager.Instance.IsCanKnockedOver)
        {
            presoAI.ChangeState(new TPPresoIdleState(presoAI));
            return;
        }

        float distance = Vector3.Distance(presoAI.transform.position, target.position);
        bool isInZone = presoAI.IsWithinTagZone(target.position);

        if (distance <= presoAI.tagDistance && isInZone && Time.time - lastTagTime > tagCooldown)
        {
            if (RaycastHitsTarget())
            {
                Debug.Log("Preso AI: Tagged the attacker!");
                // You may want to call a method on the attacker here (like OnTagged())
                lastTagTime = Time.time;
            }
        }

        // Optionally: return to chase if target is too far or not in zone
        if (distance > presoAI.tagDistance || !isInZone)
        {
            presoAI.ChangeState(new TPPresoChaseState(presoAI));
        }
    }

    public void ExitState()
    {
        Debug.Log("Preso AI: Exiting Tag State");
    }

    private bool RaycastHitsTarget()
    {
        Vector3 direction = (target.position - presoAI.transform.position).normalized;
        if (Physics.Raycast(presoAI.transform.position + Vector3.up * 0.5f, direction, out RaycastHit hit, presoAI.tagDistance))
        {
            return hit.transform == target;
        }
        return false;
    }
}
