using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] WheelCollider frontRight;
    [SerializeField] WheelCollider frontLeft;
    [SerializeField] WheelCollider backRight;
    [SerializeField] WheelCollider backleft;

    [SerializeField] Transform frontRightWheel;
    [SerializeField] Transform frontLeftWheel;
    [SerializeField] Transform backRightWheel;
    [SerializeField] Transform backLeftWheel;

    public float acceleration = 500f;
    public float brakeForce = 300f;
    public float maxTurnAngel = 10f;

    private float currentAcceleration = 0f;
    private float currentBrakeForce = 0f;
    private float currentTurnAngel = 0f;

    private void FixedUpdate()
    {

        currentAcceleration = acceleration * Input.GetAxis("Vertical");

        if (Input.GetKey(KeyCode.Space))
        {
            currentBrakeForce = brakeForce;
        }
        else
        {
            currentBrakeForce = 0f;
        }

        frontRight.motorTorque = currentAcceleration;
        frontLeft.motorTorque = currentAcceleration;

        frontRight.brakeTorque = currentBrakeForce;
        frontLeft.brakeTorque = currentBrakeForce;
        backRight.brakeTorque = currentBrakeForce;
        backleft.brakeTorque = currentBrakeForce;

        
        currentTurnAngel = maxTurnAngel * Input.GetAxis("Horizontal");
        Debug.Log(currentTurnAngel);
        frontLeft.steerAngle= currentTurnAngel;
        frontRight.steerAngle= currentTurnAngel;

        UpdateWheelMesh(frontLeft,frontLeftWheel);
        UpdateWheelMesh(frontRight,frontRightWheel);
        UpdateWheelMesh(backleft,backLeftWheel);
        UpdateWheelMesh(backRight,backRightWheel);
    }

    void UpdateWheelMesh(WheelCollider wheelCollider,Transform transform)
    {
        Vector3 position;
        Quaternion rotation;
        wheelCollider.GetWorldPose(out position, out rotation);

        transform.position = position;
        transform.rotation = rotation;

    }
}
