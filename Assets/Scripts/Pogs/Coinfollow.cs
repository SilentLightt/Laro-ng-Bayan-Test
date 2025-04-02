using UnityEngine;

public class Coinfollow : MonoBehaviour
{
    public GameObject coin;
    public Vector3 offset;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        offset = transform.position - coin.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = coin.transform.position + offset;
    }
}
