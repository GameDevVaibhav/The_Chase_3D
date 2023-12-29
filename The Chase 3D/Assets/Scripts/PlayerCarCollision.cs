using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCarCollision : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;

    [SerializeField] private PlayerHealthBar healthBar;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        UpdatePlayerHealthBar();
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePlayerHealthBar();
    }

    private void OnCollisionEnter(Collision collision)
    {
        bool policeCarCollision = collision.gameObject.CompareTag("PoliceCar");

        if (policeCarCollision)
        {
            currentHealth -= 10f;
            UpdatePlayerHealthBar();
        }
    }

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

        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
    }

    private void UpdatePlayerHealthBar()
    {
        
        // Update the health bar based on the current health and max health values
        healthBar.UpdatePlayerHealthBar(maxHealth, currentHealth);
    }
}
