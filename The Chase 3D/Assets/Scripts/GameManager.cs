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
    public PoliceCarCollison policeCarCollision;

    public bool isGameOver = false;


    public void HandleGameOver()
    {
        playUi.SetActive(false);
        isGameOver=true;
        playerCarController.SetGameOverState();
        playerGun.SetGameOverState();

        

        
        if (mudPoliceSpawnner != null)
        {
            mudPoliceSpawnner.SetGameOverState();
        }

        

        gameOverUi.SetActive(true);
    }
}
