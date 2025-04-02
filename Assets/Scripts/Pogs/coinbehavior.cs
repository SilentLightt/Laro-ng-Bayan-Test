using UnityEngine;

public class coinbehavior : MonoBehaviour
{
    public GameObject coin;
    public Rigidbody coinrb;

    private void Start()
    {
        coinrb = coin.GetComponent<Rigidbody>();
    }
    public void CoinFlip()
    {
        int jumpforce = Random.Range(500, 900);
        coinrb.AddForce(0, jumpforce, 0);
        int torqx = Random.Range(120, 900);
        coinrb.AddTorque(torqx, 0, 0);
    }
}
