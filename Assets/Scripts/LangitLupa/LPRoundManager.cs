using UnityEngine;
using System;
public class LPRoundManager : MonoBehaviour
{
    public float roundDuration = 120f;
    private float timeRemaining;
    private bool roundActive = false;

    public event Action OnRoundStart;
    public event Action OnRoundEnd;

    public LPUIManager uiManager; 

    private void Start()
    {
        StartRound();
    }

    private void Update()
    {
        if (roundActive)
        {
            timeRemaining -= Time.deltaTime;
            uiManager.UpdateRoundTimer(timeRemaining); 

            if (timeRemaining <= 0)
            {
                EndRound();
            }
        }
    }

    public void StartRound()
    {
        timeRemaining = roundDuration;
        roundActive = true;
        OnRoundStart?.Invoke();
        uiManager.UpdateRoundTimer(timeRemaining); 
    }

    private void EndRound()
    {
        roundActive = false;
        OnRoundEnd?.Invoke();
        uiManager.UpdateRoundTimer(0);
    }
}
