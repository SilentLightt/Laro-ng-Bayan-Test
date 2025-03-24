using UnityEngine;
using UnityEngine.UI;
using ThrowMechanics;
public class PogThrower : MonoBehaviour
{
    [SerializeField] private GameObject pogPrefab;
    [SerializeField] private Transform throwOrigin;
    [SerializeField] private AimingSystem aimingSystem;
    [SerializeField] private Button throwButton;
    [SerializeField] private PowerMeterUI powerMeterUI; // UI component

    private PowerMeter powerMeter;
    private IEffectiveForceCalculator forceCalculator;
    private bool isCharging = false;

    public void Initialize(PowerMeter powerMeter, IEffectiveForceCalculator forceCalculator)
    {
        this.powerMeter = powerMeter;
        this.forceCalculator = forceCalculator;
    }

    private void Awake()
    {
        powerMeter = new PowerMeter(chargeRate: 5f, maxCharge: 20f);
        forceCalculator = new DefaultForceCalculator();

        throwButton.onClick.AddListener(OnThrowButtonClicked);
        powerMeterUI.ResetSlider();
    }

    private void Update()
    {
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
    }

    public void ReleaseThrow()
    {
        isCharging = false;
        aimingSystem.ClearTrajectory();

        float playerForce = powerMeter.ReleaseCharge();
        float pogPower = 50f;

        float effectiveForce = forceCalculator.CalculateForce(playerForce, pogPower);
        ThrowPog(effectiveForce);

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

    private void OnThrowButtonClicked()
    {
        if (!isCharging)
            StartCharging();
        else
            ReleaseThrow();
    }
}
