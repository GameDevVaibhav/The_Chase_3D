using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BustingArea : MonoBehaviour
{
    public Renderer circleRenderer;
    public int policeCarCount = 0;
    public float yOffset = 0.5f;
    public Transform playerCarTransform;

    private bool isBusting = false;
    private float bustingTimer = 0f;
    private float bustingDuration = 4f; // Adjust this value for the desired duration
    public BustingBar bustingBar;
    public TextMeshProUGUI bustingText;
    void Start()
    {
        bustingBar = FindObjectOfType<BustingBar>();
        if (circleRenderer != null)
        {
            circleRenderer.enabled = false;
        }

        if (playerCarTransform == null)
        {
            playerCarTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }
    private void Update()
    {
        bustingBar.UpdateBustingBar(bustingTimer, bustingDuration);
    }
    void FixedUpdate()
    {
        if (playerCarTransform != null)
        {
            Vector3 playerPosition = playerCarTransform.position;
            transform.position = new Vector3(playerPosition.x, yOffset, playerPosition.z);

            // Check if the BustingArea is active
            if (IsActive())
            {
                bustingText.gameObject.SetActive(true);
                // Start or continue the busting timer
                if (!isBusting)
                {
                    isBusting = true;
                    bustingTimer = 0f;
                }
                else
                {
                    bustingTimer += Time.fixedDeltaTime;

                    // Check if the busting duration has been reached
                    if (bustingTimer >= bustingDuration)
                    {
                        Debug.Log("Busted!");
                        
                    }
                }
            }
            else
            {
                bustingText.gameObject.SetActive(false);
                // Reset the busting timer when the area is not active
                isBusting = false;
                bustingTimer = 0f;
                //bustingBar.UpdateBustingBar(bustingTimer, bustingDuration);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PoliceCar"))
        {
            policeCarCount++;
           // Debug.Log("police count: " + policeCarCount);

            if (policeCarCount > 0)
            {
                circleRenderer.enabled = true;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PoliceCar"))
        {
            policeCarCount--;
            

            if (policeCarCount == 0)
            {
                circleRenderer.enabled = false;
            }
        }
    }

    void OnDestroy()
    {
        if (gameObject.CompareTag("PoliceCar"))
        {
            policeCarCount--;

            if (policeCarCount == 0)
            {
                circleRenderer.enabled = false;
            }
        }
    }

    public bool IsActive()
    {
        return policeCarCount > 0;
    }

}
