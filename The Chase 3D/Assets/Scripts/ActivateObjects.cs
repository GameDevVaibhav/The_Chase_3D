using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/* Most of the object are set inactive in the scene and should be activated after the 3...2...1 countdown
   so this script activates those object.
 */

public class ActivateObjects : MonoBehaviour
{
    public GameObject[] objectsToActivate;
    void Start()
    {
        foreach(GameObject obj in objectsToActivate)
        {
            obj.SetActive(true);
        }
    }

    
}
