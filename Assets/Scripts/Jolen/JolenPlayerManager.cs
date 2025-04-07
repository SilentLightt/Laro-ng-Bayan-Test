using UnityEngine;

public class JolenPlayerManager : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Transform player1SpawnPoint;
    [SerializeField] private Transform player2SpawnPoint;

    private GameObject player1;
    private GameObject player2;

    private JolenCameraFollow cameraFollow;
    private JolenTurnManager turnManager;

    private void Awake()
    {
        cameraFollow = FindFirstObjectByType<JolenCameraFollow>();
        turnManager = FindFirstObjectByType<JolenTurnManager>();
    }

    private void Start()
    {
        // Spawn Player 1 and Player 2
        player1 = Instantiate(playerPrefab, player1SpawnPoint.position, Quaternion.identity);
        player1.name = "Player1";

        player2 = Instantiate(playerPrefab, player2SpawnPoint.position, Quaternion.identity);
        player2.name = "Player2";
        player1.GetComponent<SwipeController>().isPlayer1 = true;
        player2.GetComponent<SwipeController>().isPlayer1 = false;
        // Start camera on Player 1
        cameraFollow.SetTarget(player1.transform);

        // Listen for turn end
        JolenTurnManager.OnTurnEnd += OnTurnEnd;
    }

    private void OnTurnEnd()
    {
        // Swap camera to whoever's turn it is
        if (turnManager.IsPlayerTurn)
        {
            cameraFollow.SetTarget(player1.transform);
        }
        else
        {
            cameraFollow.SetTarget(player2.transform);
        }
    }

    private void OnDestroy()
    {
        JolenTurnManager.OnTurnEnd -= OnTurnEnd;
    }
}
