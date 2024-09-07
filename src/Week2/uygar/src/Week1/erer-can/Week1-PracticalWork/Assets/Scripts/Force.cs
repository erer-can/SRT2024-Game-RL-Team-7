using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Force : MonoBehaviour
{
    public Rigidbody rb;
    public Vector3 force;

     // We marked this as "Fixed" Update because we are using it to mess with physics
    void FixedUpdate()
    {
        rb.AddForce(force*Time.deltaTime);
    }
}
