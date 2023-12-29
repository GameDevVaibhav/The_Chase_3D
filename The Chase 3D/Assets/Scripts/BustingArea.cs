using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BustingArea : MonoBehaviour
{
    public Renderer circleRenderer; // Reference to the circle Image Renderer
    public int policeCarCount = 0;
    public float yOffset = 0.5f; // Adjust this value to set the desired y-axis position
    public Transform playerCarTransform; // Reference to the player car's transform

    void Start()
    {
        // Ensure the circleRenderer is initially disabled
        if (circleRenderer != null)
        {
            circleRenderer.enabled = false;
        }

        // Assign the player car's transform if not set in the Inspector
        if (playerCarTransform == null)
        {
            playerCarTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    void FixedUpdate()
    {
        // Move the BustingArea with the player and set the y-axis position
        if (playerCarTransform != null)
        {
            Vector3 playerPosition = playerCarTransform.position;
            transform.position = new Vector3(playerPosition.x, yOffset, playerPosition.z);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the collider is the police car
        if (other.CompareTag("PoliceCar"))
        {
            // Increment the count
            policeCarCount++;
            Debug.Log("police count: " + policeCarCount);

            // If not already detected, enable the renderer
            if (policeCarCount > 0)
            {
                circleRenderer.enabled = true;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Check if the collider is the police car
        if (other.CompareTag("PoliceCar"))
        {
            // Decrement the count
            policeCarCount--;
            Debug.Log("police count: " + policeCarCount);

            // If no more police cars, disable the renderer
            if (policeCarCount == 0)
            {
                circleRenderer.enabled = false;
            }
        }
    }

    void OnDestroy()
    {
        // This method is called when the GameObject is being destroyed
        // Make sure to decrement the count when a police car is destroyed
        if (gameObject.CompareTag("PoliceCar"))
        {
            policeCarCount--;

            // If no more police cars, disable the renderer
            if (policeCarCount == 0)
            {
                circleRenderer.enabled = false;
            }
        }
    }

    public bool IsActive()
    {
        return policeCarCount > 0; // BustingArea is active if there are police cars inside
    }

}
