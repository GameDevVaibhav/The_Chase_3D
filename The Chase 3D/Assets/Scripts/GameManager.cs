using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverUi;
    public void HandleGameOver()
    {
        // Pause the game or perform any other game over actions
        Time.timeScale = 0f; // This pauses the game

        gameOverUi.SetActive(true);
    }
}
