using UnityEngine;

public class KeyboardHandler : IMovementInput
{
    private KeyCode sprintKey = KeyCode.LeftShift;
    private KeyCode jumpKey = KeyCode.Space;

    public Vector2 GetMovementInput()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
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
