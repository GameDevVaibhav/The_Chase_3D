using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCarController : MonoBehaviour
{
    public Rigidbody sphereRB;
    private float moveInput;
    public float forwardSpeed;
    public float reverseSpeed;
    private float turnInput;
    public float turnSpeed;

    private void Start()
    {
        sphereRB.transform.parent = null;
    }

    private void Update()
    {
        moveInput = Input.GetAxisRaw("Vertical");
        turnInput = Input.GetAxisRaw("Horizontal");

        moveInput *= moveInput > 0 ? forwardSpeed : reverseSpeed;

        transform.position= sphereRB.transform.position;

        float newRotation=turnInput*turnSpeed* Time.deltaTime*Input.GetAxisRaw("Vertical");
        transform.Rotate(0, newRotation, 0);
    }
    private void FixedUpdate()
    {
        sphereRB.AddForce(transform.forward * moveInput , ForceMode.Acceleration);
    }
}
