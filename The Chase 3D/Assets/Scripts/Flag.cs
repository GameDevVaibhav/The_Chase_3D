using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag : MonoBehaviour
{
    private FlagSpawnner flagSpawner;

    private void Start()
    {
        // Find the FlagSpawner script in the scene
        flagSpawner = FindObjectOfType<FlagSpawnner>();
        if (flagSpawner == null)
        {
            Debug.LogError("FlagSpawner script not found in the scene!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the collider is the player
        if (other.CompareTag("Player"))
        {
            // Notify the FlagSpawner that the flag has been collected
            flagSpawner.FlagCollected();
            Debug.Log("Flag collision");
            // Destroy the flag
            Destroy(gameObject);
        }
    }

    
}
