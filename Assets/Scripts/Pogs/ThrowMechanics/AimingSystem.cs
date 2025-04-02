using UnityEngine;

public class AimingSystem : MonoBehaviour
{
    [SerializeField] private Transform throwOrigin;
    [SerializeField] private LineRenderer trajectoryLine;

    public float maxAimAngle = 60f;
    public float minAimAngle = 0f;
    private float aimSpeed = 5f;
    private Vector3 aimDirection = Vector3.forward;

    private void Update()
    {
        AimWithMouse();
        DrawTrajectory(throwOrigin.position, aimDirection * 10f);
    }

    private void AimWithMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Vector3 direction = (hit.point - throwOrigin.position).normalized;
            direction.y = Mathf.Clamp(direction.y, Mathf.Sin(minAimAngle * Mathf.Deg2Rad), Mathf.Sin(maxAimAngle * Mathf.Deg2Rad));
            aimDirection = direction;
        }
    }

    public Vector3 GetAimingDirection()
    {
        return aimDirection.normalized;
    }

    public void DrawTrajectory(Vector3 startPosition, Vector3 initialVelocity)
    {
        trajectoryLine.positionCount = 10;
        trajectoryLine.SetPosition(0, throwOrigin.position);

        Vector3 currentPosition = startPosition;
        Vector3 currentVelocity = initialVelocity;
        float timeStep = 0.1f;
        float gravity = Physics.gravity.y;

        for (int i = 1; i < trajectoryLine.positionCount; i++)
        {
            currentVelocity.y += gravity * timeStep;
            currentPosition += currentVelocity * timeStep;
            trajectoryLine.SetPosition(i, currentPosition);
        }
    }

    public void ClearTrajectory()
    {
        trajectoryLine.positionCount = 0;
    }
}
