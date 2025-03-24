using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class FPSController : MonoBehaviourPunCallbacks
{
    public Rigidbody myRigidBody;

    void Start()
    {
        myRigidBody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (photonView.IsMine)
        {
            if (Input.GetKey(KeyCode.W))
            {
                myRigidBody.linearVelocity = new Vector3(myRigidBody.linearVelocity.x + 0.5f, myRigidBody.linearVelocity.y, myRigidBody.linearVelocity.z);
            }
            if (Input.GetKey(KeyCode.S))
            {
                myRigidBody.linearVelocity = new Vector3(myRigidBody.linearVelocity.x - 0.5f, myRigidBody.linearVelocity.y, myRigidBody.linearVelocity.z);
            }
            if (Input.GetKey(KeyCode.A))
            {
                myRigidBody.linearVelocity = new Vector3(myRigidBody.linearVelocity.x, myRigidBody.linearVelocity.y, -myRigidBody.linearVelocity.z - 0.5f);
            }
            if (Input.GetKey(KeyCode.D))
            {
                myRigidBody.linearVelocity = new Vector3(myRigidBody.linearVelocity.x, myRigidBody.linearVelocity.y, myRigidBody.linearVelocity.z + 0.5f);
            }
            if (Input.GetKey(KeyCode.Space))
            {
                myRigidBody.linearVelocity = new Vector3(myRigidBody.linearVelocity.x, myRigidBody.linearVelocity.y + 0.5f, myRigidBody.linearVelocity.z);
            }
        }
       
    }
}
