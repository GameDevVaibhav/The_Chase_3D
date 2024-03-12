using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Spawns small rocks and big rocks around the player at certain interval
    when rocks are not in the camera view for more then the time limit set then they are destroyed
 */
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

           
            SpawnRock(rockPrefab1);
        }
    }

    IEnumerator SpawnRock2Routine()
    {
        while (true)
        {
            yield return new WaitForSeconds(intervalRock2);

            
            SpawnRock(rockPrefab2);
        }
    }

    void SpawnRock(GameObject rockPrefab)
    {
        if (playerObject != null)
        {
            
            Vector3 playerPosition = playerObject.transform.position;

            Vector2 randomOffset = Random.insideUnitCircle * spawnRadius;

            
            Vector3 spawnPosition = new Vector3(playerPosition.x + randomOffset.x, 0.5f, playerPosition.z + randomOffset.y);

           
            GameObject rock = Instantiate(rockPrefab, spawnPosition, Quaternion.identity);

           
            StartCoroutine(HandleRockLifetime(rock));
        }
        else
        {
            Debug.LogError("Player object not assigned");
        }
    }

    IEnumerator HandleRockLifetime(GameObject rock)
    {
        float elapsedTime = 0f;
        float currentElapsedTime=0f;

        while (elapsedTime < rockLifetime)
        {
            
            if (!IsInCameraView(rock, Camera.main))
            {
                
                elapsedTime += Time.deltaTime;
                currentElapsedTime = elapsedTime;
            }
            else
            {
                
                elapsedTime = currentElapsedTime;
            }

            yield return null;
        }

        
        Destroy(rock);
    }

    bool IsInCameraView(GameObject obj, Camera camera)
    {
        
        Renderer renderer = obj.GetComponent<Renderer>();
        if (renderer != null)
        {
            return GeometryUtility.TestPlanesAABB(GeometryUtility.CalculateFrustumPlanes(camera), renderer.bounds);
        }

        return false;
    }
}
