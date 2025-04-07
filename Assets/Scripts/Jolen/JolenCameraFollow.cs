using UnityEngine;

public class JolenCameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0, 2, -4);
    public float followSpeed = 5f;
    public float rotationSpeed = 5f;

    private float currentYaw = 0f;

    void LateUpdate()
    {
        if (target != null)
        {
            // Get mouse input for camera rotation
            if (Input.GetMouseButton(1)) // Right-click and drag to rotate
            {
                currentYaw += Input.GetAxis("Mouse X") * rotationSpeed;
            }

            // Calculate the rotated offset
            Quaternion rotation = Quaternion.Euler(0, currentYaw, 0);
            Vector3 rotatedOffset = rotation * offset;

            // Set camera position and rotation
            Vector3 desiredPosition = target.position + rotatedOffset;
            transform.position = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);
            transform.LookAt(target);
        }
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
}
