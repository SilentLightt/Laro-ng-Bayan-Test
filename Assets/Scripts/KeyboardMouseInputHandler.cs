using UnityEngine;

public class KeyboardMovementInputHandler : IMovementInput
{
    public Vector2 GetMovementInput()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        return new Vector2(x, z);
    }
}

