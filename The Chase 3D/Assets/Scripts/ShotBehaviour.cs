using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotBehaviour : MonoBehaviour
{
    public float speed;
    public GameObject collisionExplosion;

    private Vector3 shootDirection; // Direction to move in

    void Update()
    {
        // Move the shot prefab in the specified direction
        transform.Translate(shootDirection * speed * Time.deltaTime, Space.World);
    }

    public void SetDirection(Vector3 direction)
    {
        // Set the shoot direction without normalizing to maintain speed
        shootDirection = direction;
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the shot prefab hits a police car or player car collider
        if (other.CompareTag("PoliceCar") || other.CompareTag("Player")||other.CompareTag("Cash"))
        {
           // Debug.Log("Shot Police");
            explode();
        }
        else
        {
            Destroy(gameObject,10f);
        }
    }

    void explode()
    {
        if (collisionExplosion != null)
        {
            GameObject explosion = Instantiate(
                collisionExplosion, transform.position, transform.rotation);

            // Destroy the shot prefab upon hitting a police car or player car
            Destroy(gameObject);
            Destroy(explosion, 1f);
        }
    }
}
