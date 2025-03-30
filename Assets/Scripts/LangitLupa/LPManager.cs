using UnityEngine;
using Random = UnityEngine.Random;
using System.Collections;
public class LPManager : MonoBehaviour
{
    public float roundDuration;
    private float timeLeft;
    private bool roundActive = false;

    void Start()
    {
        roundDuration = Random.Range(30f, 120f);
        timeLeft = roundDuration;
        roundActive = true;
        StartCoroutine(RoundTimer());
    }

    IEnumerator RoundTimer()
    {
        float startTime = Time.time;
        while (Time.time - startTime < roundDuration)
        {
            timeLeft = roundDuration - (Time.time - startTime);
            yield return null;
        }
        EndRound();
    }


    public void CheckGameResult()
    {
        int remainingRunners = GameObject.FindGameObjectsWithTag("Runner").Length;

        if (remainingRunners == 0)
        {
            Debug.Log("Taya Wins! +100 Coins");
            // Award Taya coins
        }
        else if (timeLeft <= 0)
        {
            Debug.Log("Runners Win! Each survivor gets 1000 points, 100 coins");
            // Award points/coins to surviving runners
        }
        EndRound();
    }

    void EndRound()
    {
        roundActive = false;
        Debug.Log("Round Over!");
        // Add logic to reset the game or return to lobby
    }

}
