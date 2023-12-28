using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceCarCollison : MonoBehaviour
{
    public GameObject explosionPrefab; // Drag your explosion prefab here
    private float collisionTimer = 0f;
    private bool isCollidingWithPoliceCar = false;
    private float currentHealth;
    private float maxHealth = 10f;
    private float shotDamage = 1f;

    public BustingArea bustingArea;

    // Reference to the PoliceHealthBar script
    [SerializeField] private PoliceHealthBar healthBar; // Assuming you have a reference to the PoliceHealthBar

    void Start()
    {
        bustingArea = FindObjectOfType<BustingArea>();
        currentHealth = maxHealth;
        healthBar.UpdateHealthBar(maxHealth, currentHealth);
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

            AudioManager.Instance.PlayExplosionSound(); // Access the AudioManager to play the explosion sound
            Destroy(gameObject);
            InstantiateExplosion();
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
        Debug.Log("shot police");
        if (shotCollision)
        {
           
            currentHealth = currentHealth - shotDamage;
            Debug.Log("currentHealth " + currentHealth);
        }
    }

    void Update()
    {
        Debug.Log("shotdamage "+shotDamage);
        
        
        //if (isCollidingWithPoliceCar)
        //{
        //    collisionTimer += Time.deltaTime;
        //    currentHealth = currentHealth - collisionTimer; 
        //    Debug.Log(currentHealth);
        //    //healthBar.UpdateHealthBar(maxHealth, currentHealth);
        //    //// Check if the collision has lasted for more than 4 seconds
        //    //if (currentHealth < 0)
        //    //{

        //    //    AudioManager.Instance.PlayExplosionSound(); // Access the AudioManager to play the explosion sound
        //    //    Destroy(gameObject);
        //    //    InstantiateExplosion();
        //    //}
        //}
        healthBar.UpdateHealthBar(maxHealth, currentHealth);
        // Check if the collision has lasted for more than 4 seconds
        if (currentHealth < 0)
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

            Debug.Log("police count: " + bustingArea.policeCarCount);
        }
    }

}
