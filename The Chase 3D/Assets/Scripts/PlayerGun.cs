using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.Rendering.SplashScreen;



/* this is attached to playergun contains Logic for Shooting at police car and cash cubes*/

public class PlayerGun : MonoBehaviour
{
    public GameObject m_shotPrefab;
    public Transform gunMesh;
    public AudioSource gunSound;
    private BustingArea bustingArea; 

    private float rotationSpeed = 5f; 
    private float shotCooldown = 0.1f; 
    private float nextShotTime;
    private int shotsFired = 0;
    private int shotLimit = 120; 
    private int shotsRemaining;
    private Camera mainCamera;
    private bool isGameOver = false;

    public TextMeshProUGUI ammoText;


    void Start()
    {
        bustingArea = FindObjectOfType<BustingArea>(); 
        nextShotTime = Time.time; 
        mainCamera = Camera.main; 
        shotsRemaining = shotLimit;
    }

    void Update()
    {
        if (!isGameOver)
        {
            if (Input.GetMouseButton(0) && Time.time >= nextShotTime && shotsRemaining > 0)
            {
                
                TryToShootAtVisiblePoliceCar();

                
                nextShotTime = Time.time + shotCooldown;   //to set the rate of fire
                shotsFired++;
                
            }

            
            if (Input.GetMouseButton(1) && Time.time >= nextShotTime && shotsRemaining > 0)
            {
                TryToShootAtVisibleCube();

                
                nextShotTime = Time.time + shotCooldown;
                shotsFired++;
                
            }
            UpdateShotsRemaining();
        }
        
        
    }

    void TryToShootAtVisiblePoliceCar()
    {
        GameObject nearestVisiblePoliceCar = FindNearestVisiblePoliceCar();
        if (nearestVisiblePoliceCar != null)
        {
            
            RotateParentTowards(nearestVisiblePoliceCar.transform.position);

            Debug.Log("Shoot at Visible Police Car");

            
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
            
            RotateParentTowards(nearestVisibleCube.transform.position);

            Debug.Log("Shoot at Visible Cube");

            
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

        
        Vector3 viewportPos = mainCamera.WorldToViewportPoint(targetObject.transform.position);

        //returns true if Cars/Cash cubes are visible inside the camera
        return viewportPos.x >= 0f && viewportPos.x <= 1f && viewportPos.y >= 0f && viewportPos.y <= 1f;
    }

    void RotateParentTowards(Vector3 targetPosition)
    {
        Vector3 gunObjectPosition = transform.parent.position;

        
        targetPosition.y = gunObjectPosition.y;

        
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
        //Debug.Log("Refill");
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

    public void SetGameOverState()
    {
        isGameOver = true;
    }
}
