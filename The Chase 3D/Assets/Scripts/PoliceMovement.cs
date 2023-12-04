using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceMovement : MonoBehaviour
{
    [SerializeField] private float chaseSpeed;
    [SerializeField] private float steer;
    [SerializeField] private float detectionRadius;
    [SerializeField] private float avoidanceRadius;

    private GameObject playerCar;
    private Rigidbody myRigidBody;
    private bool isChasing = false;

    void Start()
    {
        myRigidBody = GetComponent<Rigidbody>();
        playerCar = GameObject.FindGameObjectWithTag("Player");

        // Add a sphere collider for obstacle detection
        SphereCollider sphereCollider = gameObject.AddComponent<SphereCollider>();
        sphereCollider.radius = avoidanceRadius;
        sphereCollider.isTrigger = true;
    }

    void Update()
    {
        if (!isChasing)
        {
            // Check if the player is within the detection radius
            if (Vector3.Distance(transform.position, playerCar.transform.position) < detectionRadius)
            {
                isChasing = true;
            }
        }
    }

    void FixedUpdate()
    {
        if (true)
        {
            // Continue chasing behavior
            Vector3 pointTarget = transform.position - playerCar.transform.position;
            pointTarget.Normalize();

            float value = Vector3.Cross(pointTarget, transform.forward).y;

            myRigidBody.angularVelocity = steer * value * new Vector3(0, 1, 0);
            myRigidBody.velocity = transform.forward * chaseSpeed;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            // Adjust steering to avoid the obstacle
            AvoidObstacle(other.transform.position);
        }
    }

    void AvoidObstacle(Vector3 obstaclePosition)
    {
        Vector3 avoidDirection = transform.position - obstaclePosition;
        avoidDirection.y = 0; // Ignore vertical component
        avoidDirection.Normalize();

        float value = Vector3.Cross(avoidDirection, transform.forward).y;

        myRigidBody.angularVelocity = steer * value * new Vector3(0, 1, 0);
        myRigidBody.velocity = transform.forward * chaseSpeed;
    }
}
