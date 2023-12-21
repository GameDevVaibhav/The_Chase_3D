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
        healthBar.UpdateHealthBar(maxHealth, currentHealth);
    }

    // Update is called once per frame
    void Update()
    {

        healthBar.UpdateHealthBar(maxHealth, currentHealth);
    }
    private void OnCollisionEnter(Collision collision)
    {
        bool policeCarCollision = collision.gameObject.CompareTag("PoliceCar");
        if (policeCarCollision)
        {
            currentHealth -= 10f;


        }
    }
}
