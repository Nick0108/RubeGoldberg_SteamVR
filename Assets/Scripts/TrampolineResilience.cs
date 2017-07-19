using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrampolineResilience : MonoBehaviour {
    public float resilience = 50.0f;

    private void OnTriggerEnter(Collider other)
    {
        float v = other.attachedRigidbody.velocity.magnitude;
        if (v > 10.0f)
            v = 10.0f;
        other.attachedRigidbody.AddForce(gameObject.transform.forward * resilience * v, ForceMode.Force);
    }
}
