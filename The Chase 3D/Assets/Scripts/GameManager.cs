using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*It handles gameOverui and Pausemenu*/

public class GameManager : MonoBehaviour
{
    public GameObject gameOverUi;
    public GameObject playUi;
    public GameObject pauseMenu;
    public PlayerCarController playerCarController;
    public PlayerGun playerGun;
    private PoliceGun policeGun;
    public MudPoliceSpawnner mudPoliceSpawnner;
    public PoliceCarMovement policeCarMovement;
    public PoliceCarCollison policeCarCollision;

    public bool isGameOver = false;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            HandlePauseMenu();
        }
    }
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

    public void HandlePauseMenu()
    {
        playUi.SetActive(false);

        Time.timeScale = 0;
        pauseMenu.SetActive(true);
    }

    public void Resume()
    {
        Time.timeScale = 1;
        playUi.SetActive(true);
        pauseMenu.SetActive(false);
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
