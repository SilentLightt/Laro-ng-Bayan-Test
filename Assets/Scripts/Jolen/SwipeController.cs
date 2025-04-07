using UnityEngine;

public class SwipeController : MonoBehaviour
{
    [SerializeField] private float throwForceMultiplier = 10f;
    [SerializeField] public bool isPlayer1; // Set in inspector or via script

    private Rigidbody rb;
    private Vector2 startTouch;
    private bool isDragging = false;

    private JolenTurnManager turnManager;
    private static bool hasSpawned = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        turnManager = FindFirstObjectByType<JolenTurnManager>();
    }

    private void Start()
    {
        if (!hasSpawned)
        {
            FindFirstObjectByType<JolenSpawner>()?.Spawn();
            hasSpawned = true;
        }
    }

    private void Update()
    {
        if (!IsMyTurn()) return;

        HandleMouseSwipe();
    }

    private bool IsMyTurn()
    {
        return (isPlayer1 && turnManager.IsPlayerTurn) || (!isPlayer1 && !turnManager.IsPlayerTurn);
    }

    private void HandleMouseSwipe()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startTouch = Input.mousePosition;
            isDragging = true;
        }
        else if (Input.GetMouseButtonUp(0) && isDragging)
        {
            Vector2 endTouch = Input.mousePosition;
            isDragging = false;
            ThrowJolen(startTouch, endTouch);
        }
    }

    private void ThrowJolen(Vector2 start, Vector2 end)
    {
        Vector2 swipe = end - start;

        // Get forward direction from camera
        Camera cam = Camera.main;
        Vector3 cameraForward = cam.transform.forward;
        cameraForward.y = 0; // Flatten to horizontal plane
        cameraForward.Normalize();

        Vector3 forceDirection = (cameraForward * swipe.magnitude) + (Vector3.up * swipe.y);
        rb.AddForce(forceDirection.normalized * throwForceMultiplier, ForceMode.Impulse);

        Invoke(nameof(EndTurn), 2f);
    }


    private void EndTurn()
    {
        turnManager.EndTurn();
    }
}
