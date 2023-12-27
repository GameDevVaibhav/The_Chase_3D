using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.SplashScreen;

public class PlayerGun : MonoBehaviour
{
    public GameObject m_shotPrefab;
    private BustingArea bustingArea; // Reference to the BustingArea script

    private float shootCooldown = 0.1f; // Time in seconds between each shot
    private float nextShootTime;

    void Start()
    {
        bustingArea = FindObjectOfType<BustingArea>(); // Find the BustingArea script in the scene
        nextShootTime = Time.time; // Initialize the next shoot time
    }

    void Update()
    {
        if (bustingArea != null && bustingArea.IsActive())
        {
            // Check if the BustingArea is active and shoot automatically at the nearest police car
            TryToShootAtNearestPoliceCar();
        }
    }

    void TryToShootAtNearestPoliceCar()
    {
        if (Time.time >= nextShootTime)
        {
            GameObject nearestPoliceCar = FindNearestPoliceCar();
            if (nearestPoliceCar != null)
            {
                Debug.Log("Shoot at Nearest Police Car");

                // Instantiate and shoot the laser at the nearest police car
                GameObject laser = Instantiate(m_shotPrefab, transform.position, transform.rotation);
                laser.GetComponent<ShotBehaviour>().setTarget(nearestPoliceCar.transform.position);

                // Update the next shoot time based on the cooldown
                nextShootTime = Time.time + shootCooldown;
            }
        }
    }

    GameObject FindNearestPoliceCar()
    {
        GameObject[] policeCars = GameObject.FindGameObjectsWithTag("PoliceCar");

        GameObject nearestPoliceCar = null;
        float nearestDistance = float.MaxValue;

        foreach (GameObject policeCar in policeCars)
        {
            float distance = Vector3.Distance(transform.position, policeCar.transform.position);
            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                nearestPoliceCar = policeCar;
            }
        }

        return nearestPoliceCar;
    }

}
