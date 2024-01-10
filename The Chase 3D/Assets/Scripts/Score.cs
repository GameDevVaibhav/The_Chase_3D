using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    public TextMeshProUGUI cashText;
    public TextMeshProUGUI carDestroyedText;
    public TextMeshProUGUI cashHighScoreText;
    public TextMeshProUGUI carDestroyedHighScoreText;
    public GameObject notificationPanel;
    public TextMeshProUGUI notificationText;

    private int cashScore = 0;
    private int carDestroyed = 0;
    private int highScoreCash;
    private int highScoreCarDestroyed;

    void Start()
    {
        // Load high scores from PlayerPrefs
        highScoreCash = PlayerPrefs.GetInt("HighScoreCash", 0);
        highScoreCarDestroyed = PlayerPrefs.GetInt("HighScoreCarDestroyed", 0);

        // Update the initial score display
        UpdateScoreText();
        UpdateHighScoreText();
    }

    void UpdateScoreText()
    {
        // Update the TextMeshProUGUI components with the current scores
        cashText.text = cashScore.ToString();
        carDestroyedText.text = carDestroyed.ToString();
    }

    void UpdateHighScoreText()
    {
        // Update the TextMeshProUGUI component with the current high scores
        cashHighScoreText.text = highScoreCash.ToString();
        carDestroyedHighScoreText.text = highScoreCarDestroyed.ToString();
    }

    void ShowNotification(string message)
    {
        // Show a notification with the specified message
        notificationText.text = message;
        notificationPanel.SetActive(true);
        StartCoroutine(HideNotification());
    }

    IEnumerator HideNotification()
    {
        // Hide the notification after a delay (e.g., 2 seconds)
        yield return new WaitForSeconds(2f);
        notificationPanel.SetActive(false);
    }

    public void IncreaseCashScore(int amount)
    {
        // Increase the cash score by the specified amount
        cashScore += amount;

        // Update the score display
        UpdateScoreText();

        // Check and update high score for cash
        if (cashScore > highScoreCash)
        {
            highScoreCash = cashScore;
            PlayerPrefs.SetInt("HighScoreCash", highScoreCash);
            UpdateHighScoreText();

            // Show notification for achieving new high score
            ShowNotification("High Score!!");
        }
    }

    public void IncreaseCarDestroyed(int count)
    {
        // Increase the carDestroyed count by the specified amount
        carDestroyed += count;

        // Update the score display
        UpdateScoreText();

        // Check and update high score for carDestroyed
        if (carDestroyed > highScoreCarDestroyed)
        {
            highScoreCarDestroyed = carDestroyed;
            PlayerPrefs.SetInt("HighScoreCarDestroyed", highScoreCarDestroyed);
            UpdateHighScoreText();

            // Show notification for achieving new high score
            ShowNotification("High Score!!");
        }
    }
}
