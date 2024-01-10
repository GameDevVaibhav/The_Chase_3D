using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCarController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float steer;

    private Rigidbody myRigidBody;
    private int currentAngle;

    // Flag to check if the game is over
    private bool isGameOver = false;
    private bool gameStarted=false;

    private void Start()
    {
        myRigidBody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if(gameStarted) 
        {
            if (!isGameOver)
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
            else
            {
                // If the game is over, stop player input and make the player static
                myRigidBody.velocity = Vector3.zero;
                myRigidBody.angularVelocity = Vector3.zero;
                myRigidBody.isKinematic = true;
            }
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Reset angular velocity to prevent rotation caused by collisions
        myRigidBody.angularVelocity = Vector3.zero;
    }

    // Method to set the game over state
    public void SetGameOverState()
    {
        isGameOver = true;
    }
    public void ActivateController()
    {
        gameStarted= true;
    }
}
