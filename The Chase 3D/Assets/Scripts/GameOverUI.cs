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
    public TextMeshProUGUI flagCollectedText;

    private void Start()
    {
        UpdateCashText();
        UpdateCarDestroyedText();

        UpdateFlagCollectedText();
    }
    private void UpdateCashText()
    {
        // Retrieve the cash value from the ScoreManager.
        int cash = score.cashScore;

        // Update the Text component on the Game Over panel.
        cashText.text = " - "+cash.ToString();
    }
    private void UpdateCarDestroyedText()
    {
        int carDestroyed = score.carDestroyed;

        carDestroyedText.text = " - "+carDestroyed.ToString();
    }
    private void UpdateFlagCollectedText()
    {
        int flagCollected = score.flagCollected;
    if(flagCollectedText!= null)
        {
            flagCollectedText.text= " - "+flagCollected.ToString();
        }
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
