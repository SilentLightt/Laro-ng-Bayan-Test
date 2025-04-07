using UnityEngine;

public class JolenSwipeController : MonoBehaviour
{
    //[SerializeField] private float throwForceMultiplier = 5f;
    [SerializeField] public bool isPlayer1;
    private float swipeStartTime;
    [SerializeField] private JolenPowerMeter powerMeter; // Reference to the PowerMeter script

    private Rigidbody rb;
    private Vector2 startTouch;
    private bool isDragging = false;
    private bool hasThrown = false;

    private JolenTurnManager turnManager;
    private static bool hasSpawned = false;
    [SerializeField] private float minForce = 5f;  // Minimum force
    [SerializeField] private float maxForce = 20f; // Maximum force
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        turnManager = FindFirstObjectByType<JolenTurnManager>();
        JolenTurnManager.OnTurnEnd += ResetForNextTurn;
    }

    private void Start()
    {
        if (!hasSpawned)
        {
            FindFirstObjectByType<JolenSpawner>()?.Spawn();
            hasSpawned = true;
        }

    }

    private void OnDestroy()
    {
        JolenTurnManager.OnTurnEnd -= ResetForNextTurn;
    }

    private void Update()
    {
        if (!IsMyTurn() || hasThrown || IsPlayerMoving()) return;

        HandleMouseSwipe();
    }

    private bool IsMyTurn()
    {
        return (isPlayer1 && turnManager.IsPlayerTurn) || (!isPlayer1 && !turnManager.IsPlayerTurn);
    }

    private bool IsPlayerMoving()
    {
        return rb.linearVelocity.magnitude > 0.1f; // Check if the player is still moving
    }

    private void HandleMouseSwipe()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startTouch = Input.mousePosition;
            swipeStartTime = Time.time;
            isDragging = true;
        }
        else if (Input.GetMouseButtonUp(0) && isDragging)
        {
            Vector2 endTouch = Input.mousePosition;
            float swipeDuration = Time.time - swipeStartTime;
            isDragging = false;

            ThrowJolen(startTouch, endTouch, swipeDuration);
        }
    }

    private void ThrowJolen(Vector2 start, Vector2 end, float duration)
    {
        if (duration <= 0f) duration = 0.01f;

        hasThrown = true; // Mark that the player has thrown

        Vector2 swipe = end - start;
        float swipeStrength = swipe.magnitude / duration;
        float dynamicForce = swipeStrength * 0.005f;
        float clampedForce = Mathf.Clamp(dynamicForce, minForce, maxForce);
        if (powerMeter != null)
        {
            powerMeter.SetPower(clampedForce);
        }
        Camera cam = Camera.main;
        Vector3 cameraForward = cam.transform.forward;
        cameraForward.y = 0;
        cameraForward.Normalize();

        Vector3 forceDirection = (cameraForward * swipe.magnitude) + (Vector3.up * swipe.y);
        rb.AddForce(forceDirection.normalized * clampedForce, ForceMode.Impulse);

        Invoke(nameof(EndTurn), 1f);
    }

    private void EndTurn()
    {
        turnManager.EndTurn();
    }

    private void ResetForNextTurn()
    {
        hasThrown = false; // Reset on turn end
    }
}
//this is dynamic, depending on the users swipe control.
//private void ThrowJolen(Vector2 start, Vector2 end, float duration)
//{
//    if (duration <= 0f) duration = 0.01f; // avoid division by zero

//    Vector2 swipe = end - start;
//    float swipeStrength = swipe.magnitude / duration;

//    // Adjust to your desired sensitivity
//    float dynamicForce = swipeStrength * 0.005f;

//    // Get forward direction from camera
//    Camera cam = Camera.main;
//    Vector3 cameraForward = cam.transform.forward;
//    cameraForward.y = 0;
//    cameraForward.Normalize();

//    Vector3 forceDirection = (cameraForward * swipe.magnitude) + (Vector3.up * swipe.y);
//    rb.AddForce(forceDirection.normalized * dynamicForce, ForceMode.Impulse);

//    Invoke(nameof(EndTurn), 2f);
//}

//default control
//private void HandleMouseSwipe()
//{
//    if (Input.GetMouseButtonDown(0))
//    {
//        startTouch = Input.mousePosition;
//        isDragging = true;
//    }
//    else if (Input.GetMouseButtonUp(0) && isDragging)
//    {
//        Vector2 endTouch = Input.mousePosition;
//        isDragging = false;
//        ThrowJolen(startTouch, endTouch);
//    }
//}

//private void ThrowJolen(Vector2 start, Vector2 end)
//{
//    Vector2 swipe = end - start;

//    // Get forward direction from camera
//    Camera cam = Camera.main;
//    Vector3 cameraForward = cam.transform.forward;
//    cameraForward.y = 0; // Flatten to horizontal plane
//    cameraForward.Normalize();

//    Vector3 forceDirection = (cameraForward * swipe.magnitude) + (Vector3.up * swipe.y);
//    rb.AddForce(forceDirection.normalized * throwForceMultiplier, ForceMode.Impulse);

//    Invoke(nameof(EndTurn), 2f);
//}
//}


