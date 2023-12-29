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
        UpdateHealthBar();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHealthBar();
    }

    private void OnCollisionEnter(Collision collision)
    {
        bool policeCarCollision = collision.gameObject.CompareTag("PoliceCar");

        if (policeCarCollision)
        {
            currentHealth -= 10f;
            UpdateHealthBar();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        bool shotCollision = other.gameObject.CompareTag("Shot");
        bool healthCollision = other.gameObject.CompareTag("Health");
        if (shotCollision)
        {
            currentHealth -= 1f;
            Debug.Log("Currenthealth" + currentHealth);
            UpdateHealthBar();
        }
        if(healthCollision)
        {
            currentHealth += 10f;
        }

        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
    }

    private void UpdateHealthBar()
    {
        Debug.Log("Currenthealth" + currentHealth);
        // Update the health bar based on the current health and max health values
        healthBar.UpdateHealthBar(maxHealth, currentHealth);
    }
}
