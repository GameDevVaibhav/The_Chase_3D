using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* This Script finds the flag in the scene and points the arrow towards it*/
public class Arrow : MonoBehaviour
{
    private FlagSpawnner flagSpawner;

    // Start is called before the first frame update
    void Start()
    {
        
        flagSpawner = FindObjectOfType<FlagSpawnner>();
        if (flagSpawner == null)
        {
            Debug.LogError("FlagSpawner script not found in the scene!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        Transform flagTransform = flagSpawner.GetCurrentFlagTransform();

        // Look at the flag if it exists
        if (flagTransform != null)
        {
            transform.LookAt(flagTransform);
        }
    }
}
