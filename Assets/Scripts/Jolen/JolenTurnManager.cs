using UnityEngine;

public class JolenTurnManager : MonoBehaviour, ITurnManager
{
    public static System.Action OnTurnEnd;

    private bool isPlayerTurn = true;
    public bool IsPlayerTurn => isPlayerTurn;

    public void EndTurn()
    {
        isPlayerTurn = !isPlayerTurn;
        OnTurnEnd?.Invoke();
    }
}
