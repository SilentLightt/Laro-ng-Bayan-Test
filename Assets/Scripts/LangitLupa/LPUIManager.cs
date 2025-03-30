using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
public class LPUIManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI countdownText;
    public TextMeshProUGUI roundTimerText;
    public TextMeshProUGUI tagAlertText;

    public void UpdateScore(int score)
    {
        scoreText.text = "Score: " + score;
    }

    public void UpdateCountdown(float time)
    {
        countdownText.text = "Move in: " + Mathf.Ceil(time);
    }

    public void UpdateRoundTimer(float time)
    {
        roundTimerText.text = "Time: " + Mathf.Ceil(time);
    }

    public void ShowTagAlert(string message)
    {
        tagAlertText.text = message;
        StartCoroutine(HideTagAlert());
    }

    private IEnumerator HideTagAlert()
    {
        yield return new WaitForSeconds(2f);
        tagAlertText.text = "";
    }
}
