using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
public class CoinFlipManager : MonoBehaviour
{
    [SerializeField] private Button flipButton;
    [SerializeField] private TMP_Text resultText;
    [SerializeField] private TurnManager turnManager;
    [SerializeField] private coinbehavior Coinbehavior;
    [SerializeField] private CoinResult coinResultManager;
    [SerializeField] private Rigidbody coinRigidbody;
    [SerializeField] private GameObject coinCamera;
    [SerializeField] private GameObject coin;

    private void Start()
    {
        flipButton.onClick.AddListener(FlipCoinButton);
        resultText.text = "Flip the coin to decide first turn!";
        Coinbehavior  = FindFirstObjectByType<coinbehavior>();
        coinResultManager = FindFirstObjectByType<CoinResult>();
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

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

        while (coinRigidbody.linearVelocity.magnitude > 0.1f || coinRigidbody.angularVelocity.magnitude > 0.1f)
        {
            yield return null;
        }

        coinCamera.SetActive(false);
        coin.SetActive(false);

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
