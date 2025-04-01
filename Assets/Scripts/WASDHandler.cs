using UnityEngine;

public class WASDHandler : MonoBehaviour, IMovementInput
{
    private KeyCode sprintKey = KeyCode.LeftShift;
    private KeyCode jumpKey = KeyCode.Space;

    public Vector2 GetMovementInput()
    {
        float x = 0f;
        float z = 0f;

        // Check for W, A, S, D keys
        if (Input.GetKey(KeyCode.W)) z = 1f;  // Forward
        if (Input.GetKey(KeyCode.S)) z = -1f; // Backward
        if (Input.GetKey(KeyCode.A)) x = -1f; // Left
        if (Input.GetKey(KeyCode.D)) x = 1f;  // Right

        return new Vector2(x, z);
    }

    public bool IsSprinting()
    {
        return Input.GetKey(sprintKey);
    }

    public bool IsJumping()
    {
        return Input.GetKeyDown(jumpKey);
    }
}
