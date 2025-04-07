using TMPro;
using UnityEngine;

public class JolenUIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI player1ScoreText;
    [SerializeField] private TextMeshProUGUI player2ScoreText;

    private void OnEnable()
    {
        JolenScoringSystem.OnScoreUpdated += UpdateScoreDisplay;
    }

    private void OnDisable()
    {
        JolenScoringSystem.OnScoreUpdated -= UpdateScoreDisplay;
    }

    private void UpdateScoreDisplay(int p1Score, int p2Score)
    {
        player1ScoreText.text = $"Player 1: {p1Score}";
        player2ScoreText.text = $"Player 2: {p2Score}";
    }
}
