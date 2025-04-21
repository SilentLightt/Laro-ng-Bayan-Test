using System;
using UnityEngine;

public enum TPPlayerRole
{
    Preso,
    Attacker
}

public class TPPlayerRoleManager : MonoBehaviour
{
    public static event Action<TPPlayerRole> OnRoleAssigned;
    public TPPlayerRole CurrentRole { get; private set; }

    public void AssignRole(TPPlayerRole role)
    {
        CurrentRole = role;
        OnRoleAssigned?.Invoke(role);
    }
}
