using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject playerPrefab;
    public List<Transform> spawnPoints;
    private RoleManager roleManager;

    void Start()
    {
        roleManager = FindFirstObjectByType<RoleManager>();
        SpawnPlayers(5); // Example: Spawn 5 players
    }

    void SpawnPlayers(int playerCount)
    {
        for (int i = 0; i < playerCount; i++)
        {
            Transform spawnPoint = spawnPoints[i % spawnPoints.Count];
            GameObject player = Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
            roleManager.RegisterPlayer(player);
        }
        roleManager.AssignRoles(); // Assign roles after spawning
    }
}
