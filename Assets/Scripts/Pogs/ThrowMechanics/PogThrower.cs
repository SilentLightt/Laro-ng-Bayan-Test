using ThrowMechanics;
using UnityEngine;

public class PogThrower : MonoBehaviour
{
    [SerializeField] private GameObject pogPrefab;
    [SerializeField] private Transform throwOrigin;
    [SerializeField] private AimingSystem aimingSystem;
    [SerializeField] private PowerMeterUI powerMeterUI;

    private PowerMeter powerMeter;
    private IEffectiveForceCalculator forceCalculator;
    private bool isCharging = false;
    private bool hasThrown = false; 

    private void Awake()
    {
        powerMeter = new PowerMeter(chargeRate: 5f, maxCharge: 20f);
        forceCalculator = new DefaultForceCalculator();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartCharging();
        }

        if (Input.GetMouseButtonUp(0))
        {
            ReleaseThrow();
        }

        if (isCharging)
        {
            powerMeter.Charge(Time.deltaTime);
            powerMeterUI.UpdateSlider(powerMeter.GetChargePercentage());

            Vector3 initialVelocity = aimingSystem.GetAimingDirection() * powerMeter.GetChargePercentage();
            aimingSystem.DrawTrajectory(throwOrigin.position, initialVelocity);
        }
    }

    public void StartCharging()
    {
        isCharging = true;
        hasThrown = false; 
    }

    public void ReleaseThrow()
    {
        isCharging = false;
        aimingSystem.ClearTrajectory();

        float playerForce = powerMeter.ReleaseCharge();
        float pogPower = 50f;

        float effectiveForce = forceCalculator.CalculateForce(playerForce, pogPower);
        ThrowPog(effectiveForce);

        hasThrown = true; 

        powerMeterUI.ResetSlider();
    }

    private void ThrowPog(float force)
    {
        GameObject thrownPog = Instantiate(pogPrefab, throwOrigin.position, Quaternion.identity);
        Rigidbody rb = thrownPog.GetComponent<Rigidbody>();

        if (rb != null)
        {
            Vector3 throwDirection = aimingSystem.GetAimingDirection();
            rb.AddForce(throwDirection.normalized * force, ForceMode.Impulse);
        }
    }
    public void ResetThrowFlag()
    {
        hasThrown = false;
    }

    public bool HasThrown()
    {
        return hasThrown;
    }
}
