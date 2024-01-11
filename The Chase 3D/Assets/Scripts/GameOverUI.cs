using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    public Score score;
    public TextMeshProUGUI cashText;
    public TextMeshProUGUI carDestroyedText;

    private void Start()
    {
        UpdateCashText();
        UpdateCarDestroyedText();
    }
    private void UpdateCashText()
    {
        // Retrieve the cash value from the ScoreManager.
        int cash = score.cashScore;

        // Update the Text component on the Game Over panel.
        cashText.text = cash.ToString();
    }
    private void UpdateCarDestroyedText()
    {
        int carDestroyed = score.carDestroyed;

        carDestroyedText.text = carDestroyed.ToString();
    }
   
    public void RestartScene()
    {
        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
