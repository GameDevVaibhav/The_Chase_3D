using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Spawn PoliceCar outside the camera viewport and also checks the car destroyed score and change the number of car spawned at a time*/
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
            
            if (playerObject != null && mainCamera != null)
            {
                
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
                    
                    Vector3 spawnPosition = GetRandomPositionOutsideCamera(spawnY);

                    
                    Instantiate(carPrefab, spawnPosition, Quaternion.identity);
                }
                
            }
            else
            {
                Debug.LogError("Player object or main camera not assigned ");
            }
        }
        
    }

    Vector3 GetRandomPositionOutsideCamera(float spawnY)
    {
        
        Vector3 cameraPosition = mainCamera.transform.position;
        float fieldOfView = mainCamera.fieldOfView;

        
        Vector2 randomDirection = Random.insideUnitCircle.normalized;

        
        Vector3 spawnPosition = cameraPosition + mainCamera.transform.forward * fieldOfView * 1.2f + new Vector3(randomDirection.x, 0f, randomDirection.y) * fieldOfView * 1.2f;
        spawnPosition.y = spawnY;

        return spawnPosition;
    }

    public void SetGameOverState()
    {
        isGameOver = true;
    }
}
