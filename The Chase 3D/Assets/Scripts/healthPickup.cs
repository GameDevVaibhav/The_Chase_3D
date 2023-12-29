using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthPickup : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision)
    {
        bool playerCollision=collision.CompareTag("Player");
        
        if(playerCollision)
        {
            Destroy(gameObject);
        }
    }
    
}
