using UnityEngine;

public class TPManager : MonoBehaviour
{
    public enum GameState { Idle, Thrown, CanKnocked, Retrieve, End }
    public GameState currentState = GameState.Idle;

    public GameObject canObject;
    private Vector3 canStartPos;

    void Start()
    {
        canStartPos = canObject.transform.position;
    }

    void Update()
    {
        switch (currentState)
        {
            case GameState.Idle:
                // Wait for player to throw
                break;

            case GameState.Thrown:
                CheckCanStatus();
                break;

            case GameState.CanKnocked:
                // Allow player to retrieve slipper
                break;

            case GameState.Retrieve:
                // Guard chase logic goes here
                break;

            case GameState.End:
                // Show score, reset game, etc.
                break;
        }
    }

    void CheckCanStatus()
    {
        if (canObject.transform.position != canStartPos)
        {
            currentState = GameState.CanKnocked;
            Debug.Log("Can knocked over!");
        }
    }
    public void OnPlayerTagged()
    {
        Debug.Log("Player tagged before retrieving slipper!");
        currentState = GameState.End;
        // Trigger fail state or reduce lives
    }
    public void OnSlipperThrown()
    {
        currentState = GameState.Thrown;
    }
    public void OnSlipperRetrieved()
    {
        Debug.Log("Slipper retrieved safely!");
        currentState = GameState.End;
        // Reset round or add score here
    }
    public void ResetRound()
    {
        // Reset can, slipper, player
        canObject.transform.position = canStartPos;
        currentState = GameState.Idle;
    }
}
