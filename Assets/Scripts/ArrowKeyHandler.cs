using UnityEngine;

public class ArrowKeyHandler : MonoBehaviour, IMovementInputt
{
    private KeyCode sprintKey = KeyCode.RightShift;
    private KeyCode jumpKey = KeyCode.Keypad0;

    public Vector2 GetMovementInput()
    {
        float x = (Input.GetKey(KeyCode.LeftArrow) ? -1f : 0f) + (Input.GetKey(KeyCode.RightArrow) ? 1f : 0f);
        float z = (Input.GetKey(KeyCode.UpArrow) ? 1f : 0f) + (Input.GetKey(KeyCode.DownArrow) ? -1f : 0f);
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
