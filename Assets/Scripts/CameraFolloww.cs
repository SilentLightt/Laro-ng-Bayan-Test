using UnityEngine;

public class CameraFolloww : MonoBehaviour
{
    public Transform target;

    [Header("Camera Settings")]
    public Vector3 thirdPersonOffset = new Vector3(0, 3, -5);
    public Vector3 firstPersonOffset = new Vector3(0, 1.7f, 0.2f); // Slightly forward for realism
    public float rotationSpeed = 2f;
    public float mouseSensitivity = 2f;

    private float currentYaw = 0f;
    private float currentPitch = 15f; // Slight downward angle
    public bool isFirstPerson = false;

    private void LateUpdate()
    {
        if (!target) return;

        // Get mouse movement
        float horizontal = Input.GetAxis("Mouse X") * mouseSensitivity;
        float vertical = Input.GetAxis("Mouse Y") * mouseSensitivity;

        currentYaw += horizontal;
        currentPitch -= vertical;
        currentPitch = Mathf.Clamp(currentPitch, -20f, 60f);

        // Choose offset based on mode
        Vector3 offset = isFirstPerson ? firstPersonOffset : thirdPersonOffset;

        // Apply rotation
        Quaternion rotation = Quaternion.Euler(currentPitch, currentYaw, 0);
        Vector3 desiredPosition = target.position + rotation * offset;

        if (isFirstPerson)
        {
            // First-person: Attach camera to player's head
            transform.position = target.position + firstPersonOffset;
            transform.rotation = Quaternion.Euler(currentPitch, currentYaw, 0);
        }
        else
        {
            // Third-person: Smoothly follow the player
            transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * 5f);
            transform.LookAt(target);
        }
    }

    // New Methods to Provide Movement Direction
    public Vector3 GetCameraForward()
    {
        Vector3 forward = transform.forward;
        forward.y = 0; // Ignore vertical tilt for movement
        return forward.normalized;
    }

    public Vector3 GetCameraRight()
    {
        Vector3 right = transform.right;
        right.y = 0;
        return right.normalized;
    }

    public void ToggleCameraMode()
    {
        isFirstPerson = !isFirstPerson;
    }
}


//using UnityEngine;

//public class CameraFolloww : MonoBehaviour
//{
//    public Transform target;
//    public Vector3 offset = new Vector3(0, 3, -5);
//    public float smoothSpeed = 5f;

//    private void LateUpdate()
//    {
//        if (!target) return;

//        Vector3 desiredPosition = target.position + offset;
//        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
//        transform.LookAt(target);
//    }
//}
