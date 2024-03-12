using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

/* It keeps the count of police car entering the busting area and if the count is more than 1 then busting area  will be activated and busting timer will start
   and if the bustingtimer reach some value then player is busted.
 */
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

    public GameManager gameManager;
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

            
            if (IsActive())
            {
                bustingText.gameObject.SetActive(true);
                
                if (!isBusting)
                {
                    isBusting = true;
                    bustingTimer = 0f;
                }
                else
                {
                    bustingTimer += Time.fixedDeltaTime;

                    
                    if (bustingTimer >= bustingDuration)
                    {
                        Debug.Log("Busted!");
                        gameManager.HandleGameOver();
                    }
                }
            }
            else
            {
                bustingText.gameObject.SetActive(false);
                
                isBusting = false;
                bustingTimer = 0f;
                
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
