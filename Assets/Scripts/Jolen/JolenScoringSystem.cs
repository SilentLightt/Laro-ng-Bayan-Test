using UnityEngine;

public class JolenScoringSystem : MonoBehaviour
{
    public static System.Action<int, int> OnScoreUpdated;

    [SerializeField] public int player1Score = 0;
    [SerializeField] public int player2Score = 0;

    private JolenTurnManager turnManager;

    private void Awake()
    {
        turnManager = FindFirstObjectByType<JolenTurnManager>();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Jolen")) // Ensure your jolen GameObjects have this tag
        {
            if (turnManager.IsPlayerTurn)
            {
                player1Score++;
            }
            else
            {
                player2Score++;
            }

            OnScoreUpdated?.Invoke(player1Score, player2Score);
            Destroy(other.gameObject);

        }
    }
}
