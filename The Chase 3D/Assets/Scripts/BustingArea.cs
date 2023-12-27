using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BustingArea : MonoBehaviour
{
    public Renderer circleRenderer; // Reference to the circle Image Renderer
    public int policeCarCount = 0;

    void Start()
    {
        // Ensure the circleRenderer is initially disabled
        if (circleRenderer != null)
        {
            circleRenderer.enabled = false;
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
