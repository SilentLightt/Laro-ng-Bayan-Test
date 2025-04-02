using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CoinFlipManager : MonoBehaviour
{
    [SerializeField] private Button flipButton;
    [SerializeField] private TMP_Text resultText;
    [SerializeField] private TurnManager turnManager;
    [SerializeField] private coinbehavior Coinbehavior;
    [SerializeField] private CoinResult coinResultManager;
    [SerializeField] private Rigidbody coinRigidbody;
    [SerializeField] private GameObject coinCamera;

    private void Start()
    {
        flipButton.onClick.AddListener(FlipCoinButton);
        resultText.text = "Flip the coin to decide first turn!";
        Coinbehavior  = FindFirstObjectByType<coinbehavior>();
    }
    private void FlipCoinButton()
    {
        Coinbehavior.CoinFlip();
        flipButton.interactable = false;
        StartCoroutine(WaitForCoinResult());
    }
    private IEnumerator WaitForCoinResult()
    {
        yield return new WaitForSeconds(2f);

        // Wait until the coin stops moving
        while (coinRigidbody.linearVelocity.magnitude > 0.1f || coinRigidbody.angularVelocity.magnitude > 0.1f)
        {
            yield return null;
        }

        // Disable the coin camera once the coin flip is completed
        coinCamera.SetActive(false);

        // Get the result and update the UI
        string result = coinResultManager.GetResult();

        if (string.IsNullOrEmpty(result) || result == "Undetermined")
        {
            resultText.text = "Result: Unable to determine. Please retry.";
        }
        else
        {
            resultText.text = "Result: " + result;
            bool isPlayer1First = (result == "Heads");
            turnManager.SetFirstPlayer(isPlayer1First);
        }
    }
}
