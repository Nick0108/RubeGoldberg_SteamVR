using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallReset : MonoBehaviour {

    Transform ballTransform;
    Vector3 ballPosition;
    Rigidbody ballRig;
    public MyGameManager myGameManager;
    public static bool ballThrowable;


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
        if (collision.gameObject.CompareTag("Star")&&(gameObject.layer!=LayerMask.NameToLayer("AntiCheat")))
        {
            myGameManager.AddScore(collision);
        }
        if (collision.gameObject.CompareTag("Goal"))
        {
            if (myGameManager.CheckWin())
                myGameManager.GameWin();
            else
                BallPosReset();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ballReleaseArea"))
            ballThrowable = true;
        else
            ballThrowable = false;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("ballReleaseArea"))
            ballThrowable = false;
        else
            ballThrowable = true;
    }

    //Reset the ball
    void BallPosReset()
    {
        gameObject.transform.position = ballPosition;
        ballRig.velocity = Vector3.zero;
        ballRig.angularVelocity = Vector3.zero;
        gameObject.GetComponent<MeshRenderer>().material.color = Color.white;
        gameObject.layer = LayerMask.NameToLayer("Default");
        myGameManager.ScoreReset();
    }
}
