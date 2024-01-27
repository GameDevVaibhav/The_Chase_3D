using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FlagSpawnner : MonoBehaviour
{
    public GameObject flagPrefab; // Reference to your flag prefab
    public List<Transform> spawnPoints; // List of spawn points
    private GameObject currentFlag;
    private bool flagCollected = true;
    private int flagsDestroyedCount = 0;

    private Score scoreManager;

    private void Start()
    {
        scoreManager = FindObjectOfType<Score>();
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the player has collected the previous flag
        if (flagCollected)
        {
            // Spawn a new flag at a randomly selected spawn point
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

        // Randomly select a spawn point
        Transform selectedSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];

        // Ensure the Y-coordinate is fixed at 1f
        Vector3 spawnPosition = selectedSpawnPoint.position;
        spawnPosition.y = 5.4f;

        // Spawn the flag at the selected position
        currentFlag = Instantiate(flagPrefab, spawnPosition, Quaternion.identity);
    }

    // Call this method when the player collects the flag
    public void FlagCollected()
    {
        flagCollected = true;
        scoreManager.IncreaseFlagsCollected();
        
        // Optionally, you can destroy the current flag after it's collected
        if (currentFlag != null)
        {
            Destroy(currentFlag);
        }
    }

    // Get the transform of the current flag
    public Transform GetCurrentFlagTransform()
    {
        return currentFlag != null ? currentFlag.transform : null;
    }

    // Get the count of flags destroyed
    public int GetFlagsDestroyedCount()
    {
        return flagsDestroyedCount;
    }
}