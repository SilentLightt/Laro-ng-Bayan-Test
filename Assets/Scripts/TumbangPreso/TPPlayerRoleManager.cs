using System;
using UnityEngine;

public enum PlayerRole { Preso, Attacker }

public class TPPlayerRoleManager : MonoBehaviour
{
    public static event Action<PlayerRole> OnRoleAssigned;
    public PlayerRole CurrentRole { get; private set; }

    public void AssignRole(PlayerRole role)
    {
        CurrentRole = role;
        OnRoleAssigned?.Invoke(role);
    }
}
