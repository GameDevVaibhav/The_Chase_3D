using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCarController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float steer;

    private Rigidbody myRigidBody;
    private int currentAngle;

    private void Start()
    {
        myRigidBody= GetComponent<Rigidbody>(); 
    }

    private void FixedUpdate()
    {
        // Get the vertical input for forward/backward movement
        float moveInput = Input.GetAxis("Vertical");
        // Get the horizontal input for steering
        float steerInput = Input.GetAxis("Horizontal");

        // Calculate the forward movement
        Vector3 moveDirection = transform.forward * moveInput * speed;

        // Apply velocity for forward movement
        myRigidBody.velocity = moveDirection;

        // Calculate rotation based on steering input
        float rotation = steerInput * steer;
        Quaternion deltaRotation = Quaternion.Euler(0f, rotation, 0f);

        // Apply rotation using Rigidbody.MoveRotation
        myRigidBody.MoveRotation(myRigidBody.rotation * deltaRotation);

    }
}
