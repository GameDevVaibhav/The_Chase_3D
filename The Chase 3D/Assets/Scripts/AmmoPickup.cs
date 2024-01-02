using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    public int ammoRefillAmount = 10; // Set the amount to refill shots
    public PlayerGun playerGun; // Serialized field to store a reference to the PlayerGun script

    private void OnCollisionEnter(Collision collision)
    {
        bool isPlayer = collision.gameObject.CompareTag("Player");
        if (isPlayer)
        {
            // Refill shots to max using the stored reference to PlayerGun
            if (playerGun != null)
            {
                playerGun.RefuelShots(ammoRefillAmount);
            }

            Destroy(gameObject);
        }
    }
}