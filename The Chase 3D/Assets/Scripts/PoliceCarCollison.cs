using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceCarCollison : MonoBehaviour
{
    public GameObject explosionPrefab; // Drag your explosion prefab here
    private float collisionTimer = 0f;
    private bool isCollidingWithPoliceCar = false;
    private float currentHealth;
    private float maxHealth = 4f;

    // Reference to the PoliceHealthBar script
    [SerializeField] private PoliceHealthBar healthBar;  // Assuming you have a reference to the PoliceHealthBar

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.UpdateHealthBar(maxHealth,currentHealth);
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
            InstantiateExplosion();
            AudioManager.Instance.PlayExplosionSound(); // Access the AudioManager to play the explosion sound
            Destroy(gameObject);
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

    void Update()
    {
        if (isCollidingWithPoliceCar)
        {
            collisionTimer += Time.deltaTime;
            currentHealth = maxHealth - collisionTimer;
            healthBar.UpdateHealthBar(maxHealth,currentHealth);
            // Check if the collision has lasted for more than 4 seconds
            if (collisionTimer > 4f)
            {
                InstantiateExplosion();
                AudioManager.Instance.PlayExplosionSound(); // Access the AudioManager to play the explosion sound
                Destroy(gameObject);
            }

            
        }
    }

    void InstantiateExplosion()
    {
        if (explosionPrefab != null)
        {
            GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(explosion, 4f);
        }
    }

}
