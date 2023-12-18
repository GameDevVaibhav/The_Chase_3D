using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceCarCollison : MonoBehaviour
{
    public GameObject explosionPrefab; // Drag your explosion prefab here

    void OnCollisionEnter(Collision collision)
    {
        bool playerCollision = collision.gameObject.CompareTag("Player");
        bool obstacleCollision = collision.gameObject.CompareTag("Obstacle");

        if (playerCollision || obstacleCollision)
        {
            InstantiateExplosion();
            AudioManager.Instance.PlayExplosionSound(); // Access the AudioManager to play the explosion sound
            Destroy(gameObject);
        }
    }

    void InstantiateExplosion()
    {
        if (explosionPrefab != null)
        {
            GameObject explosion=Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(explosion, 5f);
        }
    }

}
