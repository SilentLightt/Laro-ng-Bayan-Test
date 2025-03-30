using UnityEngine;

public interface IMovementInput
{
    Vector2 GetMovementInput();
    bool IsSprinting();
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
    private CameraFolloww cameraFolloww;

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float sprintMultiplier = 1.5f;
    [SerializeField] private float gravity = -9.81f;

    private Vector3 velocity;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        movementInput = new KeyboardMovementInputHandler();

        // Automatically find CameraFolloww in the scene
        cameraFolloww = FindFirstObjectByType<CameraFolloww>();

        if (cameraFolloww == null)
        {
            Debug.LogError("CameraFolloww script not found! Player movement may not work as expected.");
        }
    }

    private void Update()
    {
        Move();
        ApplyGravity();
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
    }

    private void ApplyGravity()
    {
        if (characterController.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        else
        {
            velocity.y += gravity * Time.deltaTime;
        }

        characterController.Move(velocity * Time.deltaTime);
    }

    public void SetMovementInput(IMovementInput newInput)
    {
        movementInput = newInput;
    }
}


//using UnityEngine;

//public interface IMovementInput
//{
//    Vector2 GetMovementInput();
//    bool IsSprinting();
//}

//public interface IMovable
//{
//    void Move();
//}

//[RequireComponent(typeof(CharacterController))]
//public class PlayerMovement : MonoBehaviour, IMovable
//{
//    private CharacterController characterController;
//    private IMovementInput movementInput;

//    [SerializeField] private float moveSpeed = 5f;
//    [SerializeField] private float sprintMultiplier = 1.5f;
//    [SerializeField] private float gravity = -9.81f;
//    private Vector3 velocity;

//    private void Awake()
//    {
//        characterController = GetComponent<CharacterController>();
//        movementInput = new KeyboardMovementInputHandler();
//    }

//    private void Update()
//    {
//        Move();
//        ApplyGravity();
//    }

//    public void Move()
//    {
//        Vector2 input = movementInput.GetMovementInput();
//        float speed = moveSpeed * (movementInput.IsSprinting() ? sprintMultiplier : 1f);

//        Vector3 moveDirection = new Vector3(input.x, 0, input.y).normalized;
//        characterController.Move(moveDirection * speed * Time.deltaTime);
//    }

//    private void ApplyGravity()
//    {
//        if (characterController.isGrounded && velocity.y < 0)
//        {
//            velocity.y = -2f;
//        }
//        else
//        {
//            velocity.y += gravity * Time.deltaTime;
//        }

//        characterController.Move(velocity * Time.deltaTime);
//    }

//    public void SetMovementInput(IMovementInput newInput)
//    {
//        movementInput = newInput;
//    }
//}
