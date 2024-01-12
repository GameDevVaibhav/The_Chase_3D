using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private FlagSpawnner flagSpawner;

    // Start is called before the first frame update
    void Start()
    {
        // Find the FlagSpawner script in the scene
        flagSpawner = FindObjectOfType<FlagSpawnner>();
        if (flagSpawner == null)
        {
            Debug.LogError("FlagSpawner script not found in the scene!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Get the transform of the current flag
        Transform flagTransform = flagSpawner.GetCurrentFlagTransform();

        // Look at the flag if it exists
        if (flagTransform != null)
        {
            transform.LookAt(flagTransform);
        }
    }
}
