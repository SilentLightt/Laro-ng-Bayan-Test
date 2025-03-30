using UnityEngine;

public class PlayerRole : MonoBehaviour, IRoleAssignable
{
    private bool isTaya;

    public void AssignRole(bool isTaya)
    {
        this.isTaya = isTaya;
        gameObject.tag = isTaya ? "Taya" : "Runner";
        Debug.Log($"{gameObject.name} is now a {(isTaya ? "Taya" : "Runner")}");
        Debug.Log($"GameObject active state: {gameObject.activeSelf}");

    }
}
