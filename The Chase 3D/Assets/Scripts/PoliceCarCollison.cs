using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PoliceCarCollison : MonoBehaviour
{
    public GameObject explosionPrefab; // Drag your explosion prefab here
   
    private bool isCollidingWithPoliceCar = false;
    private float currentPoliceHealth;
    private float maxPoliceHealth = 10f;
    private float shotDamage = 1f;
    private Score scoreManager;
    public BustingArea bustingArea;
    

    public GameOverUI gameOverUI;

    // Reference to the PoliceHealthBar script
    [SerializeField] private PoliceHealthBar healthBar; // Assuming you have a reference to the PoliceHealthBar

    void Start()
    {
        scoreManager = FindObjectOfType<Score>();
        bustingArea = FindObjectOfType<BustingArea>();
        currentPoliceHealth = maxPoliceHealth;
        healthBar.UpdatePoliceHealthBar(maxPoliceHealth, currentPoliceHealth);
    }

    void OnCollisionEnter(Collision collision)
    {
        bool playerCollision = collision.gameObject.CompareTag("Player");
        bool obstacleCollision = collision.gameObject.CompareTag("Obstacle");
        bool policeCarCollision = collision.gameObject.CompareTag("PoliceCar");

        if (policeCarCollision)
        {
            isCollidingWithPoliceCar = true;
        }


        if ((playerCollision || obstacleCollision) && !isCollidingWithPoliceCar)
        {
            gameOverUI = FindObjectOfType<GameOverUI>();
            if (gameOverUI == null)
            {
                AudioManager.Instance.PlayExplosionSound(); // Access the AudioManager to play the explosion sound
                Destroy(gameObject);
                InstantiateExplosion();
            }
            
        }
    }

    void OnCollisionExit(Collision collision)
    {
        bool policeCarCollision = collision.gameObject.CompareTag("PoliceCar");

        if (policeCarCollision)
        {
            isCollidingWithPoliceCar = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        bool shotCollision = other.gameObject.CompareTag("Shot");
        
        if (shotCollision)
        {
           
            currentPoliceHealth = currentPoliceHealth - shotDamage;
            
        }
    }

    void Update()
    {
        
        
        
       
        healthBar.UpdatePoliceHealthBar(maxPoliceHealth, currentPoliceHealth);
        
        if (currentPoliceHealth < 0)
        {

            AudioManager.Instance.PlayExplosionSound(); // Access the AudioManager to play the explosion sound
            Destroy(gameObject);
            InstantiateExplosion();
        }
    }

    void InstantiateExplosion()
    {
        if (explosionPrefab != null)
        {
            GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(explosion, 2f);
        }
    }

    void OnDestroy()
    {
        scoreManager.IncreaseCarDestroyed(1);
        // Check if BustingArea is found
        if (bustingArea != null)
        {
            // Decrement the count, but ensure it never goes below zero
            bustingArea.policeCarCount = Mathf.Max(0, bustingArea.policeCarCount - 1);

            // If no more police cars, disable the renderer
            if (bustingArea.policeCarCount == 0)
            {
                bustingArea.circleRenderer.enabled = false;
            }

            
        }
    }

   

}
