using UnityEngine;

public class SlipperThrower : MonoBehaviour
{
    public GameObject slipperPrefab;
    public Transform throwPoint;
    public float throwForce = 10f;
    public float slipperCooldown = 1f;

    private bool canThrow = true;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canThrow)
        {
            ThrowSlipper();
        }
    }

    void ThrowSlipper()
    {
        GameObject slipper = Instantiate(slipperPrefab, throwPoint.position, throwPoint.rotation);
        Rigidbody rb = slipper.GetComponent<Rigidbody>();
        rb.AddForce(throwPoint.forward * throwForce, ForceMode.Impulse);

        canThrow = false;
        Invoke(nameof(ResetThrow), slipperCooldown);
    }

    void ResetThrow()
    {
        canThrow = true;
    }
}
