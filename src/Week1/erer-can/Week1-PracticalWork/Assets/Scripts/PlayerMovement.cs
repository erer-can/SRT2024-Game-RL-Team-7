using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rb;
    public float defaultForce;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("w")) {
            rb.AddForce(-defaultForce * Time.deltaTime, 0, 0,ForceMode.VelocityChange);
        }
        if (Input.GetKey("a")) {
            rb.AddForce(0, 0, -defaultForce * Time.deltaTime,ForceMode.VelocityChange);
        }
        if (Input.GetKey("s")) {
            rb.AddForce(defaultForce * Time.deltaTime, 0, 0,ForceMode.VelocityChange);
        }
        if (Input.GetKey("d")) {
            rb.AddForce(0, 0, defaultForce * Time.deltaTime,ForceMode.VelocityChange);
        }

    }
}
