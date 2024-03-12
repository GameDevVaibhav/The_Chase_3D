using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Spawn ammo pickup inside certain radius*/
public class AmmoSpawnner : MonoBehaviour
{
    public GameObject ammo;

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
            
            Vector3 randomOffset = Random.onUnitSphere * spawnRadius;
            Vector3 randomPosition = playerTransform.position + randomOffset;
            randomPosition.y = 1f; 

            Instantiate(ammo, randomPosition, Quaternion.identity);

            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
