using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class JolenGameOverManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI player1FinalScoreText;
    [SerializeField] private TextMeshProUGUI player2FinalScoreText;
    [SerializeField] private Button retryButton;
    [SerializeField] private Button MainMenuButton;
    private JolenScoringSystem jolenScoringSystem;
    private void Start()
    {
        // Set the final score texts
        jolenScoringSystem = FindFirstObjectByType<JolenScoringSystem>();
        player1FinalScoreText.text = "Player 1: " + jolenScoringSystem.player1Score;
        player2FinalScoreText.text = "Player 2: " + jolenScoringSystem.player2Score;

        // Setup retry button functionality
        retryButton.onClick.AddListener(ReloadGame);
        MainMenuButton.onClick.AddListener(MainMenu);
    }

    private void ReloadGame()
    {
        // Load the main menu or restart the game
        SceneManager.LoadScene("GameJolenPC");
    }
    private void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");

    }
}
