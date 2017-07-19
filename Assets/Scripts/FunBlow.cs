using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunBlow : MonoBehaviour {

    public float BlowForce = 50.0f;
    public Transform FunBlade;

    private void OnTriggerStay(Collider other)
    {
        FunBlade.Rotate(0, 0, BlowForce/5.0f);
        float BlowDistance = Vector3.Distance(gameObject.transform.position, other.transform.position);
        other.attachedRigidbody.AddForce(gameObject.transform.forward.normalized*BlowForce*(-1.0f) / (BlowDistance * BlowDistance));
        //other.attachedRigidbody.AddForce(gameObject.transform.forward.normalized * -50f / Mathf.Sqrt(BlowDistance));
    }
}
