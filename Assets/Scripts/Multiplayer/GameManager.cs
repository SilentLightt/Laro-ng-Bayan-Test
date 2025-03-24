using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class GameManager : MonoBehaviour
{
    public GameObject checkpoint1;
    public GameObject checkpoint2;
    public GameObject checkpoint3;

    void Start()
    {
        int rndLocation = Random.Range(1, 4);
        switch (rndLocation)
        {
            case 1:
                PhotonNetwork.Instantiate("player", checkpoint1.transform.position, Quaternion.identity);
                break;

            case 2:
                PhotonNetwork.Instantiate("player", checkpoint2.transform.position, Quaternion.identity);
                break;

            case 3:
                PhotonNetwork.Instantiate("player", checkpoint3.transform.position, Quaternion.identity);
                break;
        }
    }
}
