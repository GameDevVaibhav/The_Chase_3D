using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIchase : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float steer;
    [SerializeField] private int numRays = 5;
    [SerializeField] private float rayAngleIncrement = 45f;
    [SerializeField] private float raycastRange = 10f;
    [SerializeField] private float reducedSpeed = 5f;

    private GameObject playerCar;
    private Rigidbody myRigidBody;

    void Start()
    {
        myRigidBody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (playerCar == null)
        {
            playerCar = GameObject.FindGameObjectWithTag("Player");
            return;
        }

        // Check for obstacles using raycasting
        bool obstacleDetected = false;
        Vector3 obstacleDirection = Vector3.zero;

        for (int i = 0; i < numRays; i++)
        {
            Vector3 rayDirection = Quaternion.Euler(0, i * rayAngleIncrement, 0) * transform.forward;

            // Draw the ray for debugging
            Debug.DrawRay(transform.position, rayDirection * raycastRange, Color.red);

            if (Physics.Raycast(transform.position, rayDirection, out RaycastHit hit, raycastRange))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    // Ignore player car
                    continue;
                }

                obstacleDetected = true;
                obstacleDirection += hit.normal;
            }
        }

        if (obstacleDetected)
        {
            obstacleDirection.Normalize();

            // Adjust steering and velocity to avoid the obstacle
            Vector3 avoidanceDirection = Quaternion.LookRotation(obstacleDirection, Vector3.up).eulerAngles;
            myRigidBody.angularVelocity = steer * avoidanceDirection.normalized;
            myRigidBody.velocity = avoidanceDirection.normalized * reducedSpeed;
        }
        else
        {
            // Follow the player car normally
            Vector3 pointTarget = playerCar.transform.position - transform.position;
            pointTarget.Normalize();

            myRigidBody.angularVelocity = steer * Vector3.Cross(transform.forward, pointTarget);
            myRigidBody.velocity = transform.forward * speed;
        }
    }
}
