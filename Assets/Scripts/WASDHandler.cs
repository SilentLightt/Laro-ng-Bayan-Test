using UnityEngine;

public class WASDHandler : MonoBehaviour, IMovementInput
{
    private KeyCode sprintKey = KeyCode.LeftShift;
    private KeyCode jumpKey = KeyCode.Space;

    public Vector2 GetMovementInput()
    {
        float x = Input.GetAxis("Horizontal"); // A/D keys
        float z = Input.GetAxis("Vertical");   // W/S keys
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
