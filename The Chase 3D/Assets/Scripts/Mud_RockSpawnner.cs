using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mud_RockSpawnner : MonoBehaviour
{
    public GameObject rockPrefab1;
    public GameObject rockPrefab2;
    public GameObject playerObject;
    public float spawnRadius = 50f;
    public float intervalRock1 = 5f;
    public float intervalRock2 = 7f;
    public float rockLifetime = 10f;

    void Start()
    {
        StartCoroutine(SpawnRock1Routine());
        StartCoroutine(SpawnRock2Routine());
    }

    IEnumerator SpawnRock1Routine()
    {
        while (true)
        {
            yield return new WaitForSeconds(intervalRock1);

            // Spawn Rock 1
            SpawnRock(rockPrefab1);
        }
    }

    IEnumerator SpawnRock2Routine()
    {
        while (true)
        {
            yield return new WaitForSeconds(intervalRock2);

            // Spawn Rock 2
            SpawnRock(rockPrefab2);
        }
    }

    void SpawnRock(GameObject rockPrefab)
    {
        if (playerObject != null)
        {
            // Get the player's position
            Vector3 playerPosition = playerObject.transform.position;

            // Generate a random offset within the spawn radius
            Vector2 randomOffset = Random.insideUnitCircle * spawnRadius;

            // Calculate the spawn position
            Vector3 spawnPosition = new Vector3(playerPosition.x + randomOffset.x, 0.5f, playerPosition.z + randomOffset.y);

            // Instantiate the rock prefab at the calculated position
            GameObject rock = Instantiate(rockPrefab, spawnPosition, Quaternion.identity);

            // Start a coroutine to check if the rock is out of the camera view and handle its lifetime
            StartCoroutine(HandleRockLifetime(rock));
        }
        else
        {
            Debug.LogError("Player object not assigned in the inspector!");
        }
    }

    IEnumerator HandleRockLifetime(GameObject rock)
    {
        float elapsedTime = 0f;
        float currentElapsedTime=0f;

        while (elapsedTime < rockLifetime)
        {
            // Check if the rock is outside the camera's view
            if (!IsInCameraView(rock, Camera.main))
            {
                // If outside camera view, increment the elapsed time
                elapsedTime += Time.deltaTime;
                currentElapsedTime = elapsedTime;
            }
            else
            {
                // If inside camera view, reset the elapsed time
                elapsedTime = currentElapsedTime;
            }

            yield return null;
        }

        // Destroy the rock after its lifetime, and if it's outside the camera view
        Destroy(rock);
    }

    bool IsInCameraView(GameObject obj, Camera camera)
    {
        // Check if the object is within the camera's frustum
        Renderer renderer = obj.GetComponent<Renderer>();
        if (renderer != null)
        {
            return GeometryUtility.TestPlanesAABB(GeometryUtility.CalculateFrustumPlanes(camera), renderer.bounds);
        }

        return false;
    }
}
