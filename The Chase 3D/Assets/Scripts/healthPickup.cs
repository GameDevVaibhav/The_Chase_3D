using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthPickup : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision)
    {
        bool playerCollision=collision.CompareTag("Player");
        Debug.Log("trigger");
        if(playerCollision)
        {
            Destroy(gameObject);
        }
    }
    
}
