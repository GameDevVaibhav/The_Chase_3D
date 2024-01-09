using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceCarMovement : MonoBehaviour
{
    [SerializeField] private float chaseSpeed;
    [SerializeField] private float steer;
    [SerializeField] private float detectionRadius;

    [Header("Sensors")]
    public float sensorLength = 3f;
    public Vector3 frontSen = new Vector3(0f, 0.2f, 2.15f);
    public float frontSideSen = 0.8f;
    public float frontSenAngle = 30f;

    public bool avoiding = false;
    private float avoidSteerMultiplier = 0f;
    private float avoidSteerSmooth = 0f;
    private float smoothTime = 0.5f;


    private GameObject playerCar;
    private Rigidbody myRigidBody;
    public bool isChasing = false;
    private bool isGameOver = false;

    void Start()
    {
        myRigidBody = GetComponent<Rigidbody>();
        playerCar = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {
        if (!isGameOver)
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
        
    }
    void FixedUpdate()
    {
        Sensors();
        if (isChasing)
        {
            if (avoiding)
            {
                // Perform avoidance behavior
                AvoidObstacle();
            }
            else
            {
                // Continue chasing behavior
                Vector3 pointTarget = transform.position - playerCar.transform.position;
                pointTarget.Normalize();

                float value = Vector3.Cross(pointTarget, transform.forward).y;

                myRigidBody.angularVelocity = steer * value * new Vector3(0, 1, 0);
                myRigidBody.velocity = transform.forward * chaseSpeed;
            }
        }
    }
    void AvoidObstacle()
    {
        // Rotate the police car to avoid the obstacle
        avoidSteerSmooth = Mathf.Lerp(avoidSteerSmooth, avoidSteerMultiplier, Time.deltaTime);

        myRigidBody.angularVelocity = 50 * avoidSteerSmooth * new Vector3(0, 1, 0);

        // You may also adjust the forward velocity to slow down when avoiding
         myRigidBody.velocity = transform.forward * (chaseSpeed * 0.5f);
    }

    void Sensors()
    {
        RaycastHit hit;
        Vector3 sensorStartPos = transform.position;
        sensorStartPos += transform.forward * frontSen.z;
        sensorStartPos += transform.up * frontSen.y;
        //float avoidMultipler = 0f;
        avoiding = false;


        //rightsen
        sensorStartPos += transform.right * frontSideSen;
        if (Physics.Raycast(sensorStartPos, transform.forward, out hit, sensorLength))
        {
            Debug.DrawLine(sensorStartPos, hit.point);
            if (hit.collider.CompareTag("Obstacle"))
            {
                avoiding = true;
                avoidSteerMultiplier =- 5f;
            }

        }

        //rightanglesen
         if (Physics.Raycast(sensorStartPos, Quaternion.AngleAxis(frontSenAngle, transform.up) * transform.forward, out hit, sensorLength))
        {
            Debug.DrawLine(sensorStartPos, hit.point);
            if (hit.collider.CompareTag("Obstacle"))
            {
                avoiding = true;
                avoidSteerMultiplier =- 2.5f;
            }

        }

        //leftsen
        sensorStartPos -= 2 * transform.right * frontSideSen;
        if (Physics.Raycast(sensorStartPos, transform.forward, out hit, sensorLength))
        {
            Debug.DrawLine(sensorStartPos, hit.point);
            if (hit.collider.CompareTag("Obstacle"))
            {
                avoiding = true;
                avoidSteerMultiplier = 5f;
            }

        }

        //leftangle
         if (Physics.Raycast(sensorStartPos, Quaternion.AngleAxis(-frontSenAngle, transform.up) * transform.forward, out hit, sensorLength))
        {
            Debug.DrawLine(sensorStartPos, hit.point);
            if (hit.collider.CompareTag("Obstacle"))
            {
                avoiding = true;
                avoidSteerMultiplier = 2.5f;
            }

        }

       
       


    }
    public void SetGameOverState()
    {
        isGameOver = true;
    }


}