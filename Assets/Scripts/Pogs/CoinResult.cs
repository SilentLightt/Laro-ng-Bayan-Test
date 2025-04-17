using UnityEngine;

public class CoinResult : MonoBehaviour
{
    private bool resultDetermined = false;
    private string coinResult = "Undetermined";
    public GameObject Heads;
    public GameObject Tails;
    private void OnTriggerStay(Collider other)
    {
        //if (!resultDetermined)
        //{
        //    if (other.gameObject == Heads)
        //    {
        //        coinResult = "Heads";
        //    }
        //    else if (other.gameObject == Tails)
        //    {
        //        coinResult = "Tails";
        //    }
        //    resultDetermined = true;
        //}
        if (!resultDetermined)
        {
            if (Heads)
            {
                coinResult = "Heads";
            }
            else if (Tails)
            {
                coinResult = "Tails";
            }
            resultDetermined = true;
        }
    }
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (!resultDetermined)
    //    {
    //        if (other.gameObject == Heads)
    //        {
    //            coinResult = "Heads";
    //        }
    //        else if (other.gameObject == Tails)
    //        {
    //            coinResult = "Tails";
    //        }
    //        resultDetermined = true;
    //    }
    //}
    public string GetResult()
    {
        return coinResult;
    }
}