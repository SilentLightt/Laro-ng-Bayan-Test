using System.Collections.Generic;
using UnityEngine;

public interface IRoleAssignable
{
    void AssignRole(bool isTaya);
}


public class RoleManager : MonoBehaviour
{
    private List<GameObject> players = new List<GameObject>();

    public void RegisterPlayer(GameObject player)
    {
        players.Add(player);
    }

    public void AssignRoles()
    {
        if (players.Count == 0) return;

        // Randomly choose one Taya
        int tayaIndex = Random.Range(0, players.Count);

        for (int i = 0; i < players.Count; i++)
        {
            bool isTaya = (i == tayaIndex);
            players[i].GetComponent<IRoleAssignable>()?.AssignRole(isTaya);
        }
    }
}
