using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceGun : MonoBehaviour
{
    public GameObject m_shotPrefab;
    public Transform gunMesh; // Reference to the gun mesh
    private GameObject playerCar;

    public float shootingDistance = 50f; // The distance at which the police car can shoot
    private float rotationSpeed = 5f; // Speed of rotation towards the player car
    private float shootCooldown = 1f; // Time in seconds between each shot
    private float nextShootTime;

    void Start()
    {
        playerCar = GameObject.FindGameObjectWithTag("Player"); // Find the player car by tag
        nextShootTime = Time.time; // Initialize the next shoot time
    }

    void Update()
    {
        // Check if the player car is within shooting distance and shoot automatically
        TryToShootAtPlayerCar();
    }

    void TryToShootAtPlayerCar()
    {
        if (Time.time >= nextShootTime && playerCar != null)
        {
            float distanceToPlayer = Vector3.Distance(gunMesh.position, playerCar.transform.position);

            if (distanceToPlayer <= shootingDistance)
            {
                // Rotate the gun mesh towards the player car using LookAt
                RotateGunMeshTowardsPlayerCar();

                Debug.Log("Shoot at Player Car");

                // Calculate adjusted shot speed based on player car's speed
                float playerCarSpeed = playerCar.GetComponent<Rigidbody>().velocity.magnitude;
                float adjustedShotSpeed = m_shotPrefab.GetComponent<ShotBehaviour>().speed + playerCarSpeed*0.3f;

                // Instantiate and shoot the laser at the player car with adjusted speed
                GameObject laser = Instantiate(m_shotPrefab, transform.position, transform.rotation);
                laser.GetComponent<ShotBehaviour>().SetDirection(playerCar.transform.position - gunMesh.position);
                laser.GetComponent<ShotBehaviour>().speed = adjustedShotSpeed;
                Debug.Log("Shotspeed" + adjustedShotSpeed);
                // Update the next shoot time based on the cooldown
                nextShootTime = Time.time + shootCooldown;
            }
        }
    }

    void RotateGunMeshTowardsPlayerCar()
    {
        // Use Transform.LookAt to make the gun mesh look at the player car
        gunMesh.LookAt(playerCar.transform.position);
    }
}
