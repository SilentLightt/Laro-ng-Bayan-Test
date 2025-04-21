using System.Collections.Generic;
using UnityEngine;

public class RoleAssigner : MonoBehaviour
{
    [SerializeField] private List<GameObject> players;

    public void AssignRoles()
    {
        int presoIndex = Random.Range(0, players.Count);

        for (int i = 0; i < players.Count; i++)
        {
            var role = (i == presoIndex) ? players[i].AddComponent<PresoRole>() : players[i].AddComponent<AttackerRole>();
            role.InitializeRole();
        }
    }
}
