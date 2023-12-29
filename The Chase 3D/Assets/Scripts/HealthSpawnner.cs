using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSpawnner : MonoBehaviour
{
    public GameObject healthPrefab;
    public float spawnInterval = 5f; 
    public float spawnRadius = 50f;

    private Transform playerTransform;

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(SpawnHealth());
    }

    IEnumerator SpawnHealth()
    {
        while (true)
        {
            Vector3 randomOffset = Random.onUnitSphere * spawnRadius;
            Vector3 randomPosition = playerTransform.position + randomOffset;
            randomPosition.y = 1f;

            Instantiate(healthPrefab, randomPosition, Quaternion.identity);

            yield return new WaitForSeconds(spawnInterval);
        }
        
    }
}
