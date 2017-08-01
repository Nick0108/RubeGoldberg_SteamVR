using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour {

    public MyGameManager myGameManager;
    private Transform starTrans;
    private void Start()
    {
        starTrans = GetComponent<Transform>();
        if(myGameManager == null)
        {
            myGameManager = FindObjectOfType<MyGameManager>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Throwable") && other.gameObject.layer !=LayerMask.NameToLayer("AntiCheat"))
        {
            myGameManager.AddScore(starTrans);
        }
    }
}
