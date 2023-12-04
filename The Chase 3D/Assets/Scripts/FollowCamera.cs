using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    private GameObject player;
    public float movementSpeed = 5f;
    public float rotationSpeed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(player != null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        CameraMovement();
    }

    void CameraMovement()
    {
        if (player == null)
        {
            return;
        }

        float playerRotationY = player.transform.rotation.eulerAngles.y;
        float rotation;
        float offset = 42f;

        //if (playerRotationY >= 180 && playerRotationY < 360)
        //{
        //    rotation = 180f;
        //}
        //else if (playerRotationY >= 180 && playerRotationY < 270)
        //{
        //    rotation = 180f;
        //}
        //else if (playerRotationY >= 270 && playerRotationY < 360)
        //{
        //    rotation = 270f;
        //}
        //else
        //{
        //    rotation = 0f;
        //}

        Vector3 offsetVector = Quaternion.Euler(0f, 0, 0f) * new Vector3(0f, 42f, -offset);
        Vector3 targetPosition = player.transform.position + offsetVector;

        // Smoothly interpolate position
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * movementSpeed);

        // Smoothly interpolate rotation
        Quaternion targetRotation = Quaternion.Euler(46f, 0, 0f);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);

    }
}
