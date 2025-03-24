using UnityEngine;
using UnityEngine.UI;

public class PowerMeterUI : MonoBehaviour
{
    [SerializeField] private Slider powerMeterSlider;

    private void Start()
    {
        Hide();
    }

    public void UpdateSlider(float chargePercentage)
    {
        if (powerMeterSlider != null)
        {
            powerMeterSlider.gameObject.SetActive(true);
            powerMeterSlider.value = chargePercentage;
        }
    }

    public void ResetSlider()
    {
        if (powerMeterSlider != null)
        {
            powerMeterSlider.value = 0;
            Hide();
        }
    }

    private void Hide()
    {
        if (powerMeterSlider != null)
            powerMeterSlider.gameObject.SetActive(false);
    }
}
