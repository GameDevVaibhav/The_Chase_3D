using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Attched to Ammo and it check collision and refill the Shots*/
public class AmmoPickup : MonoBehaviour
{
    public int ammoRefillAmount = 10; 
    public PlayerGun playerGun; 

    private void Start()
    {
        playerGun=FindObjectOfType<PlayerGun>();
    }
    private void OnTriggerEnter(Collider collision)
    {
        bool isPlayer = collision.gameObject.CompareTag("Player");
        if (isPlayer)
        {
            
            // Refill shots to max using the stored reference to PlayerGun
            if (playerGun != null)
            {
                Debug.Log("Refill");
                playerGun.RefuelShots(ammoRefillAmount);
            }

            Destroy(gameObject);
        }
    }
}
