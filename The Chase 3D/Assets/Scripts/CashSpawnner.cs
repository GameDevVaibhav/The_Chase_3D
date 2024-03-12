using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Spawn Cash cubes inside certain spawn radius */

public class CashSpawnner : MonoBehaviour
{
    public GameObject cashPrefab;
    public float spawnInterval = 5f; 
    public float spawnRadius = 50f; 

    private Transform playerTransform;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform; 
        StartCoroutine(SpawnCashBox());
    }

    IEnumerator SpawnCashBox()
    {
        while (true)
        {
            // Spawn a cube at a random position around the player within the spawn radius
            Vector3 randomOffset = Random.onUnitSphere * spawnRadius;
            Vector3 randomPosition = playerTransform.position + randomOffset;
            randomPosition.y = 3f; 

            Instantiate(cashPrefab, randomPosition, Quaternion.identity);

            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
