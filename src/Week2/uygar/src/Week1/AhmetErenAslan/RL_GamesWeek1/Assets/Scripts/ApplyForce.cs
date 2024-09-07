using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class ApplyForce : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private float forceMagnitude = 1.0f;


    private void Start() {
        rb = GetComponent<Rigidbody>();
    }
    private void FixedUpdate() {
        rb.AddForce(Vector3.up * forceMagnitude);
    }

    
}
