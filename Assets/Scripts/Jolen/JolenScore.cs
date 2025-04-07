using UnityEngine;

public class JolenScore : MonoBehaviour
{
    private int player1Score = 0;
    private int player2Score = 0;

    public int CurrentScore => player1Score + player2Score;

    public JolenScore GetPlayer1Score() => this; // return this as player 1 score
    public JolenScore GetPlayer2Score() => this; // return this as player 2 score

    public void AddScore(int points)
    {
        // Add score logic (decide which player to add points to based on game rules)
        if (player1Score > player2Score) // example condition, adjust based on the turn system
        {
            player1Score += points;
        }
        else
        {
            player2Score += points;
        }
    }
}
