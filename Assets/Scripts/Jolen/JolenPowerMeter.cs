using UnityEngine;
using UnityEngine.UI;

public class JolenPowerMeter : MonoBehaviour
{
    [SerializeField] private Image powerMeterImage; // Reference to the Image component
    [SerializeField] private float minForce = 5f; // Minimum force value
    [SerializeField] private float maxForce = 20f; // Maximum force value

    private void UpdatePowerMeter(float currentForce)
    {
        if (powerMeterImage != null)
        {
            // Calculate the fill amount based on the current force between min and max force
            float fillAmount = Mathf.InverseLerp(minForce, maxForce, currentForce);
            powerMeterImage.fillAmount = fillAmount;
        }
    }

    // Method to set the power meter externally (to be called from JolenSwipeController)
    public void SetPower(float currentForce)
    {
        UpdatePowerMeter(currentForce);
    }
}
