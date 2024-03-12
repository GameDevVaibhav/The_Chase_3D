using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/* check the collision of police car and instantiate explosionvfx and audio
    when shots hit the police car police car health is reduced.
    it also access the busting are to update the count if any police car is destroyed inside the area.
 */
public class PoliceCarCollison : MonoBehaviour
{
    public GameObject explosionPrefab; 
   
    private bool isCollidingWithPoliceCar = false;
    private float currentPoliceHealth;
    private float maxPoliceHealth = 10f;
    private float shotDamage = 1f;
    private Score scoreManager;
    public BustingArea bustingArea;
    

    public GameOverUI gameOverUI;

   
    [SerializeField] private PoliceHealthBar healthBar; 

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
                AudioManager.Instance.PlayExplosionSound(); 
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

            AudioManager.Instance.PlayExplosionSound(); 
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
        
        if (bustingArea != null)
        {
            
            bustingArea.policeCarCount = Mathf.Max(0, bustingArea.policeCarCount - 1);

            
            if (bustingArea.policeCarCount == 0)
            {
                bustingArea.circleRenderer.enabled = false;
            }

            
        }
    }

   

}
