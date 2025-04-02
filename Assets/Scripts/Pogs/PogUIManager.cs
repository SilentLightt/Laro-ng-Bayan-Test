using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text turnText;
    [SerializeField] private TMP_Text timeText;

    public void UpdateTurnText(string turnMessage)
    {
        turnText.text = turnMessage;
    }

    public void UpdateTimeText(float timeRemaining)
    {
        timeText.text = "Time: " + Mathf.Ceil(timeRemaining).ToString() + "s";
    }
}
