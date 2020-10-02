using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody))]
public class ForceRigidbody : MonoBehaviour {
    [SerializeField] private float force;
    [SerializeField] private float lift;
    [SerializeField] private bool applyAtStart;

    private void Start() {
        if (applyAtStart)
            ApplyForce(transform.forward);
    }

    public void ApplyForce(Vector3 forward) {
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        rigidbody.AddForce(forward * force + Vector3.up * lift, ForceMode.Force);
    }
}
