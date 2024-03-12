using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Checks collision with player and calls flag collected method*/

public class Flag : MonoBehaviour
{
    private FlagSpawnner flagSpawner;

    private void Start()
    {
        
        flagSpawner = FindObjectOfType<FlagSpawnner>();
        if (flagSpawner == null)
        {
            Debug.LogError("FlagSpawner  not found in the scene");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            
            flagSpawner.FlagCollected();
            Debug.Log("Flag collision");
           
            Destroy(gameObject);
        }
    }

    
}
