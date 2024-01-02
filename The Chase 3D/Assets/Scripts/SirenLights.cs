using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SirenLights : MonoBehaviour
{
    public GameObject redLight;
    public GameObject blueLight;
    public float blinkInterval = 0.3f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Siren());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Siren()
    {
        yield return new WaitForSeconds(blinkInterval);

        redLight.SetActive(true);
        blueLight.SetActive(false);

        yield return new WaitForSeconds(blinkInterval);
        redLight.SetActive(false);
        blueLight.SetActive(true);

        StartCoroutine(Siren());
    }
}
