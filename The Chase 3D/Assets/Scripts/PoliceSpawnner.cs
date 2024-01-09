using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceSpawnner : MonoBehaviour
{
    public GameObject policeCarPrefab; // Drag your police car prefab here
    public Transform[] spawnPoints;    // Add your spawn points in the Inspector
    public float spawnInterval = 5f;   // Time in seconds between each spawn

    private GameObject[] lastSpawnedCars;
    private bool isGameOver = false;

    void Start()
    {
        lastSpawnedCars = new GameObject[spawnPoints.Length];
        InvokeRepeating("SpawnPoliceCars", 0f, spawnInterval);
    }

    void SpawnPoliceCars()
    {
        if(!isGameOver)
        {
            for (int i = 0; i < spawnPoints.Length; i++)
            {
                Transform spawnPoint = spawnPoints[i];

                if (lastSpawnedCars[i] == null || Vector3.Distance(lastSpawnedCars[i].transform.position, spawnPoint.position) > 2f)
                {
                    GameObject newCar = Instantiate(policeCarPrefab, spawnPoint.position, spawnPoint.rotation);
                    lastSpawnedCars[i] = newCar;
                }
            }
        }
       
    }

    public void SetGameOverState()
    {
        isGameOver = true;
    }
}
