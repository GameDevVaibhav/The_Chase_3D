using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* It takes shoot direction from playergun/policegun script and moves it towards that direction */
public class ShotBehaviour : MonoBehaviour
{
    public float speed;
    public GameObject collisionExplosion;

    private Vector3 shootDirection; 

    void Update()
    {
        
        transform.Translate(shootDirection * speed * Time.deltaTime, Space.World);
    }

    public void SetDirection(Vector3 direction)
    {
       
        shootDirection = direction;
    }

    void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("PoliceCar") || other.CompareTag("Player")||other.CompareTag("Cash"))
        {
           
            explode();
        }
        if (other.CompareTag("Obstacle"))
        {
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

            
            Destroy(gameObject);
            Destroy(explosion, 1f);
        }
    }
}
