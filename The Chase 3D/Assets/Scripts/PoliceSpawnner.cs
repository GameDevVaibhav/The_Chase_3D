using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* This Script is used to spawn police car. Spawn points are already fixed on the map and police car will be spawned there when game starts
    Also if the car has moved from its spawnpoint then new car will be spawned on that empty spawn point.
    
 */

public class PoliceSpawnner : MonoBehaviour
{
    public GameObject policeCarPrefab; 
    public Transform[] spawnPoints;    
    public float spawnInterval = 5f;   

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
