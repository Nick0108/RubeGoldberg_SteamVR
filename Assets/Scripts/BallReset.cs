using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallReset : MonoBehaviour {

    Transform ballTransform;
    Vector3 ballPosition;
    Rigidbody ballRig;


    void Start () {
        ballTransform = GetComponent<Transform>();
        ballPosition = ballTransform.position;
        ballRig = GetComponent<Rigidbody>();
        BallPosReset();
    }

    //when the ball hit the ground
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            BallPosReset();
        }
    }

    //Reset the ball
    void BallPosReset()
    {
        gameObject.transform.position = ballPosition;
        ballRig.velocity = Vector3.zero;
        ballRig.angularVelocity = Vector3.zero;
    }
}
