using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SwipeController : MonoBehaviour
{
    [SerializeField] private float throwForceMultiplier = 10f;

    private Rigidbody rb;
    private Vector2 startTouch;
    private bool isDragging = false;

    private JolenTurnManager turnManager;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        turnManager = FindFirstObjectByType<JolenTurnManager>();
    }

    private void Start()
    {
        FindFirstObjectByType<JolenSpawner>()?.Spawn();
    }

    private void Update()
    {
        if (!turnManager.IsPlayerTurn) return;

        HandleMouseSwipe();
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
        Vector3 direction = new Vector3(swipe.x, swipe.y, swipe.magnitude);
        rb.AddForce(direction.normalized * throwForceMultiplier, ForceMode.Impulse);
        Invoke(nameof(EndTurn), 2f);
    }

    private void EndTurn()
    {
        turnManager.EndTurn();
    }
}
