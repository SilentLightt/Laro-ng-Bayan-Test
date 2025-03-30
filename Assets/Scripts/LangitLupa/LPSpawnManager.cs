using System.Collections.Generic;
using UnityEngine;

public class LPSpawnManager : MonoBehaviour
{
    public List<Transform> spawnPoints;
    public GameObject playerPrefab;
    public int playerCount = 4;
    public LPRoleManager roleManager;
    private List<GameObject> players = new List<GameObject>();

    private void Start()
    {
        SpawnPlayers();
    }

    private void SpawnPlayers()
    {
        if (playerPrefab == null || spawnPoints.Count == 0) return;

        for (int i = 0; i < playerCount; i++)
        {
            GameObject newPlayer = Instantiate(playerPrefab, spawnPoints[i % spawnPoints.Count].position, Quaternion.identity);
            players.Add(newPlayer);
        }

        AssignFirstTaya();
    }

    private void AssignFirstTaya()
    {
        if (players.Count > 0)
        {
            int randomIndex = UnityEngine.Random.Range(0, players.Count);
            roleManager.InitializeRoles(players, players[randomIndex]);
        }
    }
}