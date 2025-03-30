using UnityEngine;

public interface IMovementInput
{
    Vector2 GetMovementInput();
}

public interface IMovable
{
    void Move();
}

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour, IMovable
{
    private CharacterController characterController;
    private IMovementInput movementInput;

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float gravity = -9.81f;
    private Vector3 velocity;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        movementInput = new KeyboardMovementInputHandler(); // Default input strategy
    }

    private void Update()
    {
        Move();
        ApplyGravity();
    }

    public void Move()
    {
        Vector2 input = movementInput.GetMovementInput();
        Vector3 moveDirection = new Vector3(input.x, 0, input.y);
        characterController.Move(moveDirection * moveSpeed * Time.deltaTime);
    }

    private void ApplyGravity()
    {
        if (characterController.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Small downward force to stay grounded
        }
        else
        {
            velocity.y += gravity * Time.deltaTime;
        }

        characterController.Move(velocity * Time.deltaTime);
    }


    public void SetMovementInput(IMovementInput newInput)
    {
        movementInput = newInput; // Allows for dynamic input switching
    }
}

