using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallReset : MonoBehaviour {

    Transform ballTransform;
    Vector3 ballPosition;
    Rigidbody ballRig;
    
    public MyGameManager myGameManager;
    
    
    public static bool ballThrowable;
    public static bool isOnHand;
    //public bool isThrow = true;

    void Start () {
        ballThrowable = false;
        isOnHand = false;
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
        if (collision.gameObject.CompareTag("Goal"))
        {
            if (myGameManager.CheckWin())
                myGameManager.GameWin();
            else
                BallPosReset();
        }
    }


    //Reset the ball
    void BallPosReset()
    {
        gameObject.transform.SetParent(null);
        gameObject.transform.position = ballPosition;
        ballRig.velocity = Vector3.zero;
        ballRig.angularVelocity = Vector3.zero;
        gameObject.GetComponent<MeshRenderer>().material.color = Color.white;
        gameObject.layer = LayerMask.NameToLayer("Default");
        myGameManager.ScoreReset();
        ballThrowable = false;
        isOnHand = false;
    }

    private void Update()
    {
        if (isOnHand)
        {
            if (!ballThrowable)
            {
                gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
                gameObject.gameObject.layer = LayerMask.NameToLayer("AntiCheat");
            }else
            {
                gameObject.GetComponent<MeshRenderer>().material.color = Color.white;
                gameObject.layer = LayerMask.NameToLayer("Default");
            }
        }

        /*if (ballThrowable == true)
            isThrow = true;
        else
            isThrow = false;*/
    }


}
