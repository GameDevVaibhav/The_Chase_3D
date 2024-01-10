using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
