using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverUi;
    public GameObject playUi;
    public PlayerCarController playerCarController;
    public PlayerGun playerGun;
    private PoliceGun policeGun;
    public MudPoliceSpawnner mudPoliceSpawnner;
    public PoliceCarMovement policeCarMovement;

   
    public void HandleGameOver()
    {
        playUi.SetActive(false);

        playerCarController.SetGameOverState();
        playerGun.SetGameOverState();

        policeGun = FindObjectOfType<PoliceGun>();
        policeGun.SetGameOverState();
        if(mudPoliceSpawnner != null) 
        {
            mudPoliceSpawnner.SetGameOverState();
        }
        
        policeCarMovement= FindObjectOfType<PoliceCarMovement>();
        if(policeCarMovement!= null)
        {
            policeCarMovement.SetGameOverState();
        }

        gameOverUi.SetActive(true);
    }
}
