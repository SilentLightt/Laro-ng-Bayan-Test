using UnityEngine;

public class KeyboardMovementInputHandler : IMovementInput
{
    private KeyCode sprintKey = KeyCode.LeftShift;

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
}
