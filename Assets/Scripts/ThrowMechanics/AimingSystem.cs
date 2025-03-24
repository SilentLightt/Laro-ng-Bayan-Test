using UnityEngine;

public class AimingSystem : MonoBehaviour
{
    [SerializeField] private Transform throwOrigin;
    [SerializeField] private LineRenderer trajectoryLine;
    [SerializeField] private Joystick aimingJoystick; // Joystick Reference

    public float maxAimAngle = 60f; // Limits vertical aiming
    public float minAimAngle = 0f; // Prevents aiming downward
    private float aimSpeed = 5f; // Smoothing speed

    private Vector3 aimDirection = Vector3.forward;

    private void Update()
    {
        if (aimingJoystick == null) return;

        // Get joystick input
        float horizontalInput = aimingJoystick.Horizontal;
        float verticalInput = aimingJoystick.Vertical;

        if (Mathf.Abs(horizontalInput) > 0.1f || Mathf.Abs(verticalInput) > 0.1f)
        {
            // Convert joystick movement into a 3D aiming direction
            Vector3 joystickDirection = new Vector3(horizontalInput, verticalInput, 1f).normalized;

            // Get angle in degrees and clamp it
            float angle = Mathf.Atan2(joystickDirection.y, joystickDirection.z) * Mathf.Rad2Deg;
            angle = Mathf.Clamp(angle, minAimAngle, maxAimAngle);

            // Convert back to direction
            float angleRadians = angle * Mathf.Deg2Rad;
            aimDirection = new Vector3(joystickDirection.x, Mathf.Sin(angleRadians), Mathf.Cos(angleRadians)).normalized;
        }

        DrawTrajectory(throwOrigin.position, aimDirection * 10f);
    }

    public Vector3 GetAimingDirection()
    {
        return aimDirection.normalized;
    }

    public void DrawTrajectory(Vector3 startPosition, Vector3 initialVelocity)
    {
        trajectoryLine.positionCount = 10; // Number of trajectory points
        trajectoryLine.SetPosition(0, throwOrigin.position); // Start at throw origin

        Vector3 currentPosition = startPosition;
        Vector3 currentVelocity = initialVelocity;
        float timeStep = 0.1f; // Time step for trajectory simulation
        float gravity = Physics.gravity.y;

        for (int i = 1; i < trajectoryLine.positionCount; i++)
        {
            currentVelocity.y += gravity * timeStep; // Apply gravity
            currentPosition += currentVelocity * timeStep;
            trajectoryLine.SetPosition(i, currentPosition);
        }
    }

    public void ClearTrajectory()
    {
        trajectoryLine.positionCount = 0;
    }
}
