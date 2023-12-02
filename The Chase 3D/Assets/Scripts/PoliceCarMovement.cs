using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceCarMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float steer;

    private GameObject playerCar;
    private Rigidbody myRigidBody;
    // Start is called before the first frame update
    void Start()
    {
        myRigidBody= GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(playerCar==null)
        {
            playerCar = GameObject.FindGameObjectWithTag("Player");
            return;
        }

        Vector3 pointTarget=transform.position-playerCar.transform.position;
        pointTarget.Normalize();

        float value=Vector3.Cross(pointTarget, transform.forward).y;

        myRigidBody.angularVelocity = steer * value * new Vector3(0, 1, 0);
        myRigidBody.velocity=transform.forward*speed;
    }
}
