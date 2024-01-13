using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MudPoliceSpawnner : MonoBehaviour
{
    public GameObject carPrefab;
    public float spawnInterval = 3f;

    public GameObject playerObject;
    private Camera mainCamera;
    private bool isGameOver = false;

    private Score score;
    private int carDestroyed = 0;

    void Start()
    {
        mainCamera = Camera.main;
        score = FindObjectOfType<Score>();

        if (mainCamera == null)
        {
            Debug.LogError("Main camera not found!");
        }

        InvokeRepeating("SpawnCar", 0f, spawnInterval);
    }
    private void Update()
    {
        carDestroyed = score.carDestroyed;
        
    }

    void SpawnCar()
    {
        if(!isGameOver)
        {
            // Check if the player object and main camera are assigned
            if (playerObject != null && mainCamera != null)
            {
                // Set the Y-axis position for the car
                float spawnY = 0.5f;
                int noOfCars = 1;
                if (carDestroyed > 10&&carDestroyed<20)
                {
                    noOfCars = 2;
                }
                else if (carDestroyed >= 20)
                {
                    noOfCars = 3;
                }
                for(int i = 0; i < noOfCars; i++)
                {
                    // Calculate a spawn position outside the camera view
                    Vector3 spawnPosition = GetRandomPositionOutsideCamera(spawnY);

                    // Instantiate the car prefab at the calculated position
                    Instantiate(carPrefab, spawnPosition, Quaternion.identity);
                }
                
            }
            else
            {
                Debug.LogError("Player object or main camera not assigned in the inspector!");
            }
        }
        
    }

    Vector3 GetRandomPositionOutsideCamera(float spawnY)
    {
        // Get the camera's position and field of view
        Vector3 cameraPosition = mainCamera.transform.position;
        float fieldOfView = mainCamera.fieldOfView;

        // Determine a random direction outside the camera view
        Vector2 randomDirection = Random.insideUnitCircle.normalized;

        // Calculate a position just outside the camera view with fixed Y-axis
        Vector3 spawnPosition = cameraPosition + mainCamera.transform.forward * fieldOfView * 1.2f + new Vector3(randomDirection.x, 0f, randomDirection.y) * fieldOfView * 1.2f;
        spawnPosition.y = spawnY;

        return spawnPosition;
    }

    public void SetGameOverState()
    {
        isGameOver = true;
    }
}
