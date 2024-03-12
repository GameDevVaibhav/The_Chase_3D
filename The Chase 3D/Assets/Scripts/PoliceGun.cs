using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/* Checks the distance between the police and player and shoot at it if distance is less then set distance*/

public class PoliceGun : MonoBehaviour
{
    public GameObject m_shotPrefab;
    public Transform gunMesh; 
    private GameObject playerCar;
    public AudioSource gunSound;

    public float shootingDistance = 50f; 
    private float rotationSpeed = 5f; 
    private float shootCooldown = 1f; 
    private float nextShootTime;
    private bool isGameOver = false;

    public GameOverUI gameOverUI;

    void Start()
    {
        playerCar = GameObject.FindGameObjectWithTag("Player"); 
       
        nextShootTime = Time.time; 
    }

    void Update()
    {
        gameOverUI=FindObjectOfType<GameOverUI>();
        
        if (gameOverUI ==null)
        {
            TryToShootAtPlayerCar();
        }
        
        
    }

    void TryToShootAtPlayerCar()
    {
        if (Time.time >= nextShootTime && playerCar != null)
        {
            float distanceToPlayer = Vector3.Distance(gunMesh.position, playerCar.transform.position);
            
            if (distanceToPlayer <= shootingDistance)
            {
                
                RotateGunMeshTowardsPlayerCar();

               

                
                float playerCarSpeed = playerCar.GetComponent<Rigidbody>().velocity.magnitude;
                float adjustedShotSpeed = m_shotPrefab.GetComponent<ShotBehaviour>().speed + playerCarSpeed*0.3f;

                
                GameObject laser = Instantiate(m_shotPrefab, transform.position, transform.rotation);
                laser.GetComponent<ShotBehaviour>().SetDirection(playerCar.transform.position - gunMesh.position);
                laser.GetComponent<ShotBehaviour>().speed = adjustedShotSpeed;
               
                nextShootTime = Time.time + shootCooldown;
                gunSound.Play();
            }
        }
    }

    void RotateGunMeshTowardsPlayerCar()
    {
        
        gunMesh.LookAt(playerCar.transform.position);
    }

    public void SetGameOverState()
    {
        isGameOver = true;
    }
}
