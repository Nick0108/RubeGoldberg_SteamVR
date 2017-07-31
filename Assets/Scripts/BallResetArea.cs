using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallResetArea : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Throwable"))
        {
            BallReset.ballThrowable = !BallReset.ballThrowable;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Throwable"))
            BallReset.ballThrowable = !BallReset.ballThrowable;
    }
}
