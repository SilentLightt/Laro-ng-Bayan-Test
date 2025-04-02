using TMPro;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    [SerializeField] private GameObject player1;
    [SerializeField] private GameObject player2;
    [SerializeField] private Camera player1Camera;
    [SerializeField] private Camera player2Camera;
    [SerializeField] private PogThrower player1Thrower;
    [SerializeField] private PogThrower player2Thrower;
    [SerializeField] private AimingSystem player1AimingSystem;
    [SerializeField] private AimingSystem player2AimingSystem;
    [SerializeField] private float turnTimeLimit = 30f;

    [SerializeField] private TMP_Text turnText;   
    [SerializeField] private TMP_Text timeText;   

    private bool isPlayer1Turn = true;
    private bool isTurnActive = false;
    private float turnTimeRemaining;

    private void Start()
    {
        StartNewTurn();
    }

    private void Update()
    {
        if (isTurnActive)
        {
            turnTimeRemaining -= Time.deltaTime;
            UpdateTimeText(); 

            if (turnTimeRemaining <= 0f || HasCurrentPlayerThrown())
            {
                EndTurn();
            }
        }
    }

    private bool HasCurrentPlayerThrown()
    {
        if (isPlayer1Turn)
        {
            return player1Thrower.HasThrown();
        }
        else
        {
            return player2Thrower.HasThrown(); 
        }
    }

    private void StartNewTurn()
    {
        isTurnActive = true;
        turnTimeRemaining = turnTimeLimit;
        UpdateTurnUI();

        if (isPlayer1Turn)
        {
            EnablePlayer1();
            DisablePlayer2();
        }
        else
        {
            EnablePlayer2();
            DisablePlayer1();
        }
    }

    private void EndTurn()
    {
        isTurnActive = false;

        isPlayer1Turn = !isPlayer1Turn;

        player1Thrower.ResetThrowFlag();
        player2Thrower.ResetThrowFlag();

        // Start the next turn
        StartNewTurn();
    }

    private void EnablePlayer1()
    {
        player1.SetActive(true);
        player1Camera.enabled = true;
        player1Thrower.enabled = true;
        player1AimingSystem.enabled = true;
    }

    private void DisablePlayer1()
    {
        player1.SetActive(false);
        player1Camera.enabled = false;
        player1Thrower.enabled = false;
        player1AimingSystem.enabled = false;
    }

    private void EnablePlayer2()
    {
        player2.SetActive(true);
        player2Camera.enabled = true;
        player2Thrower.enabled = true;
        player2AimingSystem.enabled = true;
    }

    private void DisablePlayer2()
    {
        player2.SetActive(false);
        player2Camera.enabled = false;
        player2Thrower.enabled = false;
        player2AimingSystem.enabled = false;
    }

    private void UpdateTurnUI()
    {
        // Update the turn display to reflect the current player's turn
        if (isPlayer1Turn)
        {
            turnText.text = "Player 1's Turn";
        }
        else
        {
            turnText.text = "Player 2's Turn";
        }
    }

    private void UpdateTimeText()
    {
        // Update the countdown timer UI to reflect the time remaining
        timeText.text = "Time: " + Mathf.Ceil(turnTimeRemaining).ToString() + "s";
    }
}
