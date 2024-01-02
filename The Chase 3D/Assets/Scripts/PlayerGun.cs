using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.Rendering.SplashScreen;

public class PlayerGun : MonoBehaviour
{
    public GameObject m_shotPrefab;
    public Transform gunMesh;
    public AudioSource gunSound;
    private BustingArea bustingArea; // Reference to the BustingArea script

    private float rotationSpeed = 5f; // Speed of rotation towards the police car
    private float shotCooldown = 0.1f; // Time in seconds between each shot
    private float nextShotTime;
    private int shotsFired = 0;
    private int shotLimit = 10; 
    private int shotsRemaining;
    private Camera mainCamera;

    public TextMeshProUGUI ammoText;


    void Start()
    {
        bustingArea = FindObjectOfType<BustingArea>(); // Find the BustingArea script in the scene
        nextShotTime = Time.time; // Initialize the next shot time
        mainCamera = Camera.main; // Find the main camera
        shotsRemaining = shotLimit;
    }

    void Update()
    {
        Debug.Log("ShotRemain" + shotsRemaining);
        
        if (Input.GetMouseButton(0) && Time.time >= nextShotTime && shotsRemaining > 0)
        {
            // Check if the BustingArea is active and shoot automatically at the nearest visible police car
            TryToShootAtVisiblePoliceCar();

            // Update the next shot time based on the cooldown
            nextShotTime = Time.time + shotCooldown;
            shotsFired++;
             // Update shots remaining after each shot
        }

        // Check if the right mouse button is pressed, enough time has passed since the last shot, and shots remaining is greater than 0
        if (Input.GetMouseButton(1) && Time.time >= nextShotTime && shotsRemaining > 0)
        {
            // Shoot at the nearest cube only if it is visible in the camera's view
            TryToShootAtVisibleCube();

            // Update the next shot time based on the cooldown
            nextShotTime = Time.time + shotCooldown;
            shotsFired++;
            //UpdateShotsRemaining(); // Update shots remaining after each shot
        }
        UpdateShotsRemaining();
    }

    void TryToShootAtVisiblePoliceCar()
    {
        GameObject nearestVisiblePoliceCar = FindNearestVisiblePoliceCar();
        if (nearestVisiblePoliceCar != null)
        {
            // Rotate the parent (GunObject) towards the nearest visible police car
            RotateParentTowards(nearestVisiblePoliceCar.transform.position);

            Debug.Log("Shoot at Visible Police Car");

            // Instantiate and shoot the laser at the nearest visible police car
            GameObject laser = Instantiate(m_shotPrefab, transform.position, transform.rotation);
            laser.GetComponent<ShotBehaviour>().SetDirection(nearestVisiblePoliceCar.transform.position - gunMesh.position);

            gunSound.Play();
        }
    }

    void TryToShootAtVisibleCube()
    {
        GameObject nearestVisibleCube = FindNearestVisibleCube();
        if (nearestVisibleCube != null)
        {
            // Rotate the parent (GunObject) towards the nearest visible cube
            RotateParentTowards(nearestVisibleCube.transform.position);

            Debug.Log("Shoot at Visible Cube");

            // Instantiate and shoot the laser at the nearest visible cube
            GameObject laser = Instantiate(m_shotPrefab, transform.position, transform.rotation);
            laser.GetComponent<ShotBehaviour>().SetDirection(nearestVisibleCube.transform.position - gunMesh.position);
            gunSound.Play();
        }
    }

    bool IsObjectVisibleInCamera(GameObject targetObject)
    {
        if (mainCamera == null || targetObject == null)
        {
            return false;
        }

        // Convert the target object's position to viewport coordinates
        Vector3 viewportPos = mainCamera.WorldToViewportPoint(targetObject.transform.position);

        // Check if the viewport coordinates fall within the camera's view (0 to 1 for both x and y)
        return viewportPos.x >= 0f && viewportPos.x <= 1f && viewportPos.y >= 0f && viewportPos.y <= 1f;
    }

    void RotateParentTowards(Vector3 targetPosition)
    {
        Vector3 gunObjectPosition = transform.parent.position;

        // Ensure the y-component is the same to prevent unwanted tilting
        targetPosition.y = gunObjectPosition.y;

        // Use Transform.LookAt to make the GunObject look at the target position
        transform.parent.LookAt(targetPosition);
    }

    GameObject FindNearestVisiblePoliceCar()
    {
        GameObject[] policeCars = GameObject.FindGameObjectsWithTag("PoliceCar");

        GameObject nearestVisiblePoliceCar = null;
        float nearestDistance = float.MaxValue;

        foreach (GameObject policeCar in policeCars)
        {
            float distance = Vector3.Distance(transform.position, policeCar.transform.position);
            if (distance < nearestDistance && IsObjectVisibleInCamera(policeCar))
            {
                nearestDistance = distance;
                nearestVisiblePoliceCar = policeCar;
            }
        }

        return nearestVisiblePoliceCar;
    }

    GameObject FindNearestVisibleCube()
    {
        GameObject[] cashBoxes = GameObject.FindGameObjectsWithTag("Cash");

        GameObject nearestVisibleCube = null;
        float nearestDistance = float.MaxValue;

        foreach (GameObject cashBox in cashBoxes)
        {
            float distance = Vector3.Distance(transform.position, cashBox.transform.position);
            if (distance < nearestDistance && IsObjectVisibleInCamera(cashBox))
            {
                nearestDistance = distance;
                nearestVisibleCube = cashBox;
            }
        }

        return nearestVisibleCube;
    }
    public void RefuelShots(int amount)
    {
        Debug.Log("Refill");
        if(shotLimit!=shotsRemaining)
        {
            shotsFired = 0;

            shotsRemaining += amount;
        }
        

       
        // shotLimit = Mathf.Min(maxShotLimit, shotLimit);
    }
    void UpdateShotsRemaining()
    {
        shotsRemaining = Mathf.Clamp(shotLimit - shotsFired, 0, shotLimit); 
        ammoText.text = shotsRemaining.ToString()+" / "+shotLimit;
    }
}
