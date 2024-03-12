using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/*This script used for updating player health. It checks the collision of Car with 3 things.
    1. PoliceCar 2.HealthObject 3.Shots
    
   And Accordingly change the health of the Car. 
 
 */

public class PlayerCarCollision : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;

    [SerializeField] private PlayerHealthBar healthBar;

    
    void Start()
    {
        currentHealth = maxHealth;
        UpdatePlayerHealthBar();
    }

    
    void Update()
    {
        UpdatePlayerHealthBar();
    }

    //collision with Police Car and Updating health
    private void OnCollisionEnter(Collision collision)
    {
        bool policeCarCollision = collision.gameObject.CompareTag("PoliceCar");

        if (policeCarCollision)
        {
            currentHealth -= 10f;
            UpdatePlayerHealthBar();
        }
    }

    //collision with shot and health object 
    private void OnTriggerEnter(Collider other)
    {
        bool shotCollision = other.gameObject.CompareTag("Shot");
        bool healthCollision = other.gameObject.CompareTag("Health");
        if (shotCollision)
        {
            currentHealth -= 1f;
            
            UpdatePlayerHealthBar();
        }
        if (healthCollision)
        {
            currentHealth += 10f;
        }

        //Clamping health so it does not go below 0
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
    }

    private void UpdatePlayerHealthBar()
    {
        //Calling UpdatePlayerHealthBar from healthbar script
        
        healthBar.UpdatePlayerHealthBar(maxHealth, currentHealth);
    }
}
