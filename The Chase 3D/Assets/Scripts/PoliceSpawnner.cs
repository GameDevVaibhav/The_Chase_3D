using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceSpawnner : MonoBehaviour
{
    public GameObject policeCarPrefab; // Drag your police car prefab here
    public Transform[] spawnPoints;    // Add your spawn points in the Inspector
    public float spawnInterval = 5f;   // Time in seconds between each spawn

    private GameObject lastSpawnedCar;

    void Start()
    {
        InvokeRepeating("SpawnPoliceCars", 0f, spawnInterval);
    }

    void SpawnPoliceCars()
    {
        foreach (Transform spawnPoint in spawnPoints)
        {
            if (lastSpawnedCar == null || Vector3.Distance(lastSpawnedCar.transform.position, spawnPoint.position) > 2f)
            {
                GameObject newCar = Instantiate(policeCarPrefab, spawnPoint.position, spawnPoint.rotation);
                lastSpawnedCar = newCar;
            }
        }
    }
}
