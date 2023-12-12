using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceCarCollison : MonoBehaviour
{
    public GameObject explosionPrefab; // Drag your explosion prefab here
    public AudioSource explosionSfx;
    void Start()
    {

    }

    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        bool playerCollision = collision.gameObject.CompareTag("Player");
        bool obstacleCollision = collision.gameObject.CompareTag("Obstacle");

        if (playerCollision || obstacleCollision)
        {
            
            InstantiateExplosion();
            Destroy(gameObject);
        }
    }

    void InstantiateExplosion()
    {
        if (explosionPrefab != null)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        }
    }


}
