using UnityEngine;
using System.Collections.Generic;

public class SwipeThrowController : MonoBehaviour
{
    private Vector2 startTouch, endTouch;
    private bool isSwiping;
    private Rigidbody jolenRb;
    public float throwForceMultiplier = 10f;
    public static System.Action OnTurnEnd;
    public bool isPlayerTurn = true;

    public Transform squareArea; // Reference to the square boundary
    public GameObject jolenPrefab; // Prefab for spawning jolens
    public int jolenCount = 5; // Number of jolens inside the square
    private List<GameObject> spawnedJolens = new List<GameObject>();

    void Start()
    {
        jolenRb = GetComponent<Rigidbody>();
        OnTurnEnd += SwitchTurn;
        SpawnJolensInsideSquare();
    }

    void OnDestroy()
    {
        OnTurnEnd -= SwitchTurn;
    }

    void Update()
    {
        if (isPlayerTurn)
        {
            HandleSwipeInput();
        }
    }

    private void HandleSwipeInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                startTouch = touch.position;
                isSwiping = true;
            }
            else if (touch.phase == TouchPhase.Ended && isSwiping)
            {
                endTouch = touch.position;
                isSwiping = false;
                ThrowJolen();
            }
        }
    }

    private void ThrowJolen()
    {
        Vector2 swipeDirection = endTouch - startTouch;
        Vector3 throwDirection = new Vector3(swipeDirection.x, swipeDirection.y, swipeDirection.magnitude);
        jolenRb.AddForce(throwDirection.normalized * throwForceMultiplier, ForceMode.Impulse);
        Invoke("EndTurn", 2f); // Simulate delay before switching turns
    }

    private void EndTurn()
    {
        OnTurnEnd?.Invoke();
    }

    private void SwitchTurn()
    {
        isPlayerTurn = !isPlayerTurn;
    }

    private void SpawnJolensInsideSquare()
    {
        if (squareArea == null || jolenPrefab == null) return;

        Vector3 areaSize = squareArea.localScale;
        Vector3 areaCenter = squareArea.position;

        for (int i = 0; i < jolenCount; i++)
        {
            Vector3 randomPosition = new Vector3(
                Random.Range(areaCenter.x - areaSize.x / 2, areaCenter.x + areaSize.x / 2),
                areaCenter.y,
                Random.Range(areaCenter.z - areaSize.z / 2, areaCenter.z + areaSize.z / 2)
            );

            GameObject newJolen = Instantiate(jolenPrefab, randomPosition, Quaternion.identity);
            spawnedJolens.Add(newJolen);
        }
    }
}
