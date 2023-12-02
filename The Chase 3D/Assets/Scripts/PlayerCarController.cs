using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCarController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float steer;
    [SerializeField] private float rotationForce;
    [SerializeField] private float rotationDamping;

    private Rigidbody myRigidBody;
    private int currentAngle;

    private void Start()
    {
        myRigidBody= GetComponent<Rigidbody>(); 
    }

    private void FixedUpdate()
    { // Get the vertical input for forward/backward movement
        float moveInput = Input.GetAxis("Vertical");
        // Get the horizontal input for steering
        float steerInput = Input.GetAxis("Horizontal");

        // Calculate the forward movement
        Vector3 moveDirection = transform.forward * moveInput * speed;

        // Apply velocity for forward movement
        myRigidBody.velocity = moveDirection;

        // Calculate rotation based on steering input
        float rotation = steerInput * steer;

        // Apply rotation using AddTorque to allow external forces to affect it
        myRigidBody.AddTorque(Vector3.up * rotation * rotationForce);

        // Apply damping to gradually reduce rotation when no steering input
        if (Mathf.Approximately(steerInput, 0f))
        {
            float dampingTorque = -myRigidBody.angularVelocity.y * rotationDamping;
            myRigidBody.AddTorque(Vector3.up * dampingTorque);
        }

    }
}
