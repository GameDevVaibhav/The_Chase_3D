using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.SplashScreen;

public class PlayerGun : MonoBehaviour
{
    public GameObject m_shotPrefab;
    public Transform gunMesh;
    private BustingArea bustingArea; // Reference to the BustingArea script

    private float rotationSpeed = 5f; // Speed of rotation towards the police car
    private float shotCooldown = 0.1f; // Time in seconds between each shot
    private float nextShotTime;

    void Start()
    {
        bustingArea = FindObjectOfType<BustingArea>(); // Find the BustingArea script in the scene
        nextShotTime = Time.time; // Initialize the next shot time
    }

    void Update()
    {
        // Check if the mouse button is held down and enough time has passed since the last shot
        if (Input.GetMouseButton(0) && Time.time >= nextShotTime)
        {
            // Check if the BustingArea is active and shoot automatically at the nearest police car
            TryToShootAtNearestPoliceCar();

            // Update the next shot time based on the cooldown
            nextShotTime = Time.time + shotCooldown;
        }
    }

    void TryToShootAtNearestPoliceCar()
    {
        GameObject nearestPoliceCar = FindNearestPoliceCar();
        if (nearestPoliceCar != null)
        {
            // Rotate the parent (GunObject) towards the nearest police car
            RotateParentTowards(nearestPoliceCar.transform.position);

            Debug.Log("Shoot at Nearest Police Car");

            // Instantiate and shoot the laser at the nearest police car
            GameObject laser = Instantiate(m_shotPrefab, transform.position, transform.rotation);
            laser.GetComponent<ShotBehaviour>().SetDirection(nearestPoliceCar.transform.position - gunMesh.position);
        }
    }

    void RotateParentTowards(Vector3 targetPosition)
    {
        Vector3 gunObjectPosition = transform.parent.position;

        // Ensure the y-component is the same to prevent unwanted tilting
        targetPosition.y = gunObjectPosition.y;

        // Use Transform.LookAt to make the GunObject look at the police car
        transform.parent.LookAt(targetPosition);
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
