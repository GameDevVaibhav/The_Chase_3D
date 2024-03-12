using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


/* Spawns a flag at any random position on the map and spawns spawn next flag only when previous flag is collected by the player*/

public class FlagSpawnner : MonoBehaviour
{
    public GameObject flagPrefab; 
    public List<Transform> spawnPoints; 
    private GameObject currentFlag;
    private bool flagCollected = true;
    private int flagsDestroyedCount = 0;

    private Score scoreManager;

    private void Start()
    {
        scoreManager = FindObjectOfType<Score>();
    }

    
    void Update()
    {
       
        if (flagCollected)
        {
            
            SpawnFlag();
            flagCollected = false;
        }
    }

    void SpawnFlag()
    {
        if (spawnPoints.Count == 0)
        {
            Debug.LogError("No spawn points assigned in the inspector!");
            return;
        }

        
        Transform selectedSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];

        
        Vector3 spawnPosition = selectedSpawnPoint.position;
        spawnPosition.y = 5.4f;

        
        currentFlag = Instantiate(flagPrefab, spawnPosition, Quaternion.identity);
    }

    
    public void FlagCollected()
    {
        flagCollected = true;
        scoreManager.IncreaseFlagsCollected();
        
        
        if (currentFlag != null)
        {
            Destroy(currentFlag);
        }
    }

    
    public Transform GetCurrentFlagTransform()
    {
        return currentFlag != null ? currentFlag.transform : null;
    }

   
    public int GetFlagsDestroyedCount()
    {
        return flagsDestroyedCount;
    }
}
