using UnityEngine;

public interface IMovementInputt
{
    Vector2 GetMovementInput();
    bool IsSprinting();
    bool IsJumping();

}

public interface IMovablee
{
    void Move();
}

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement1 : MonoBehaviour, IMovablee
{
    private CharacterController characterController;
    private IMovementInputt movementInput;
    private CameraFolloww cameraFolloww;

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float sprintMultiplier = 1.5f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float jumpHeight = 2f; // Jump strength

    [SerializeField] private Transform groundCheck; // Empty object at player's feet
    [SerializeField] private float groundDistance = 0.2f; // Ground detection distance
    [SerializeField] private LayerMask groundMask; // Layer(s) considered as ground

    private bool isGrounded;
    private Vector3 velocity;
    private void Awake()
    {
        characterController = GetComponent<CharacterController>();

        // Try to find any movement input component (WASD or Arrow Keys)
        movementInput = GetComponent<IMovementInputt>();

        if (movementInput == null)
        {
            Debug.LogError("No IMovementInput found! Attach WASDMovementHandler or ArrowKeyMovementHandler.");
        }

        cameraFolloww = FindFirstObjectByType<CameraFolloww>();

        if (cameraFolloww == null)
        {
            Debug.LogError("CameraFolloww script not found! Player movement may not work as expected.");
        }
    }

    //private void Awake()
    //{
    //    characterController = GetComponent<CharacterController>();
    //    movementInput = new KeyboardHandler();

    //    // Automatically find CameraFolloww in the scene
    //    cameraFolloww = FindFirstObjectByType<CameraFolloww>();

    //    if (cameraFolloww == null)
    //    {
    //        Debug.LogError("CameraFolloww script not found! Player movement may not work as expected.");
    //    }
    //}

    private void Update()
    {
        Move();
        ApplyGravity();
        GroundCheck();

    }
    private void GroundCheck()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        // Reset downward velocity when on the ground
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
    }
    public void Move()
    {
        Vector2 input = movementInput.GetMovementInput();
        float speed = moveSpeed * (movementInput.IsSprinting() ? sprintMultiplier : 1f);

        if (cameraFolloww != null)
        {
            Vector3 moveDirection = (cameraFolloww.GetCameraForward() * input.y +
                                     cameraFolloww.GetCameraRight() * input.x).normalized;
            characterController.Move(moveDirection * speed * Time.deltaTime);
        }
        if (movementInput.IsJumping() && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }

    private void ApplyGravity()
    {
        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);

    }

    public void SetMovementInput(IMovementInputt newInput)
    {
        movementInput = newInput;
    }
}
