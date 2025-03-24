using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{

    public GameObject player;
    public GameObject checkpoint1;
    public GameObject checkpoint2;
    public GameObject checkpoint3;

    void Start()
    {
        int rndLocation = Random.Range(1, 4);
        switch (rndLocation)
        {
            case 1:
                Instantiate(player.gameObject, checkpoint1.transform.position, Quaternion.identity);
                break;

            case 2:
                Instantiate(player.gameObject, checkpoint2.transform.position, Quaternion.identity);
                break;

            case 3:
                Instantiate(player.gameObject, checkpoint3.transform.position, Quaternion.identity);
                break;
        }
    }

}
