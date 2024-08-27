using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]  
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float forceMagnitude;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
           rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 inputVector = new Vector2(0,0);

        if (Input.GetKey(KeyCode.W)) {
            inputVector.y += 1;
        }
        if (Input.GetKey(KeyCode.S)) {
            inputVector.y += -1;
        }
        if (Input.GetKey(KeyCode.A)) {
            inputVector.x += -1;
        }
        if (Input.GetKey(KeyCode.D)) {
            inputVector.x += 1;
        }

       
        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);
        rb.AddForce(moveDir.normalized * forceMagnitude *Time.deltaTime, ForceMode.VelocityChange);

    }
}
