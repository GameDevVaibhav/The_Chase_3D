using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/* This Script is attached to Player Car. Vertical inputs (W/S) controlls forward and
 * backward movement and Horizontal Input (A/D) Controls Steer of the Car
 * there are 2 bool variables which checks if the game has started or is over 
 * and accordingly turns input on/off
 * 
 */




public class PlayerCarController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float steer;

    private Rigidbody myRigidBody;
    private int currentAngle;

    
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
                
                float moveInput = Input.GetAxis("Vertical");
                
                float steerInput = Input.GetAxis("Horizontal");

                
                Vector3 moveDirection = transform.forward * moveInput * speed;

                
                myRigidBody.velocity = moveDirection;

                
                float rotation = steerInput * steer;
                Quaternion deltaRotation = Quaternion.Euler(0f, rotation, 0f);

                
                myRigidBody.MoveRotation(myRigidBody.rotation * deltaRotation);
            }
            else
            {
                
                myRigidBody.velocity = Vector3.zero;
                myRigidBody.angularVelocity = Vector3.zero;
                myRigidBody.isKinematic = true;
            }
        }
        
    }

    //collisions were causing Car to unnecessary rotation so setting angular velocity to zero after a collision
    private void OnCollisionEnter(Collision collision)
    {
        
        myRigidBody.angularVelocity = Vector3.zero;
    }

    
    public void SetGameOverState()
    {
        isGameOver = true;
    }
    public void ActivateController()
    {
        gameStarted= true;
    }
}
