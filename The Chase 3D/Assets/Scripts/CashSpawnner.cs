using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CashSpawnner : MonoBehaviour
{
    public GameObject cashPrefab;
    public float spawnInterval = 5f; // Time between cube spawns
    public float spawnRadius = 50f; // Range around the player for cube spawns

    private Transform playerTransform;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform; // Assuming the player has the "Player" tag
        StartCoroutine(SpawnCashBox());
    }

    IEnumerator SpawnCashBox()
    {
        while (true)
        {
            // Spawn a cube at a random position around the player within the spawn radius
            Vector3 randomOffset = Random.onUnitSphere * spawnRadius;
            Vector3 randomPosition = playerTransform.position + randomOffset;
            randomPosition.y = 3f; // Set the Y-axis position to 3

            Instantiate(cashPrefab, randomPosition, Quaternion.identity);

            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
