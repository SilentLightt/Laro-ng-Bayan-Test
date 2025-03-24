using UnityEngine;

public class PowerMeter
{
    private float chargeRate;
    private float maxCharge;
    private float currentCharge = 0f;

    public PowerMeter(float chargeRate, float maxCharge)
    {
        this.chargeRate = chargeRate;
        this.maxCharge = maxCharge;
    }

    public void Charge(float deltaTime)
    {
        currentCharge += chargeRate * deltaTime;
        currentCharge = Mathf.Min(currentCharge, maxCharge);
    }

    public float ReleaseCharge()
    {
        float releasedCharge = currentCharge;
        currentCharge = 0f;
        return releasedCharge;
    }

    public float GetChargePercentage()
    {
        return currentCharge / maxCharge;
    }
}
