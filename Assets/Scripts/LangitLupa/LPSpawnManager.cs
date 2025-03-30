using System.Collections.Generic;
using UnityEngine;

public class LPSpawnManager : MonoBehaviour
{
    public List<Transform> spawnPoints;
    public List<GameObject> players;
    public LPRoleManager roleManager;

    private void Start()
    {
        SpawnPlayers();
    }

    private void SpawnPlayers()
    {
        if (players.Count == 0 || spawnPoints.Count == 0) return;

        for (int i = 0; i < players.Count; i++)
        {
            players[i].transform.position = spawnPoints[i % spawnPoints.Count].position;
        }

        AssignFirstTaya();
    }

    private void AssignFirstTaya()
    {
        if (players.Count > 0)
        {
            int randomIndex = UnityEngine.Random.Range(0, players.Count);
            roleManager.SwitchRoles(players[randomIndex]);
        }
    }
}