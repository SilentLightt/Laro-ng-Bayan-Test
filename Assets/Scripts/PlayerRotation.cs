using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    public float rotationSpeed = 720f;

    public void Rotate(Vector2 lookInput, Transform cameraTransform)
    {
        if (lookInput.sqrMagnitude < 0.1f) return;

        float targetAngle = Mathf.Atan2(lookInput.x, lookInput.y) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
        Quaternion rotation = Quaternion.Euler(0f, targetAngle, 0f);

        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
    }
}
