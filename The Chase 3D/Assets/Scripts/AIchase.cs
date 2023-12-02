using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIchase : MonoBehaviour
{
    [SerializeField]
    float moveSpeed = 20f;
    [SerializeField]
    float steer = 10f;
    [SerializeField]
    float offset;
    public Transform target;
    float input;


    Rigidbody myRigidBody;
    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody>();


    }
    void Update()
    {


    }

    // Update is called once per frame
    void FixedUpdate()
    { // Calculate the direction to the target
        Vector3 direction = (target.position - transform.position).normalized;

        // Rotate to look at the target
        transform.LookAt(target);

        // Move towards the target
        myRigidBody.velocity = direction * moveSpeed;

        // Optionally, you can also apply rotation based on the direction
        float rotationSteer = Vector3.Cross(transform.up, direction).y;
        myRigidBody.angularVelocity = new Vector3(0f, rotationSteer * steer, 0f);


    }
}
