using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;


/* This Script is attached to Play_Ui object. It gets all the score and flags collected data from playerprefs and update it in UI
 * also has  notification logic which displays "HighScore" when player makes highscore
 */
public class Score : MonoBehaviour
{
    public TextMeshProUGUI cashText;
    public TextMeshProUGUI carDestroyedText;
    public TextMeshProUGUI flagCollectedText;
    public TextMeshProUGUI notificationText;
    public int cashScore = 0;
    public int carDestroyed = 0;
    public int flagCollected = 0;
    private int cashHighScore= 0;
    private int carDestroyedHighScore = 0;
    private int flagCollectedHighScore = 0;
    private bool carHighscoreisShown= false;
    private bool cashHighscoreisShwon= false;
    private bool flagHighscoreisShown=false;

    void Start()
    {

        cashHighScore = PlayerPrefs.GetInt("CashHighscore", 0);
        carDestroyedHighScore = PlayerPrefs.GetInt("CarDestroyedHighScore", 0);
        flagCollectedHighScore = PlayerPrefs.GetInt("FlagCollectedHighScore", 0);


        Debug.Log("highscore " + cashHighScore);
        Debug.Log("Car HighScore" + carDestroyedHighScore);
        UpdateScoreText();
    }
    private void Update()
    {
        SaveHighscore();
    }
    void UpdateScoreText()
    {
        
        cashText.text = cashScore.ToString();
        carDestroyedText.text = carDestroyed.ToString();
        if(flagCollectedText!= null)
        {
            flagCollectedText.text = flagCollected.ToString();
        }
        
    }

    public void IncreaseCashScore(int amount)
    {
        
        cashScore += amount;

        
        UpdateScoreText();
    }
    public void IncreaseCarDestroyed(int count)
    {
        carDestroyed += count;
        UpdateScoreText();

    }
    public void IncreaseFlagsCollected()
    {
        flagCollected++;
        UpdateScoreText();
    }

    private void SaveHighscore()
    {

        if (cashScore > cashHighScore)
        {
            cashHighScore = cashScore;

            PlayerPrefs.SetInt("CashHighscore", cashHighScore);
            PlayerPrefs.Save();

            if (!cashHighscoreisShwon)
            {
                ShowNotification("HighScore");
                cashHighscoreisShwon= true;
            }
            

            
        }
        if(carDestroyed> carDestroyedHighScore)
        {
            carDestroyedHighScore = carDestroyed;

            PlayerPrefs.SetInt("CarDestroyedHighScore", carDestroyedHighScore);
            PlayerPrefs.Save();

            if (!carHighscoreisShown)
            {
                ShowNotification("HighScore");
                carHighscoreisShown= true;
            }
            
        }

        if (flagCollected > flagCollectedHighScore)
        {
            flagCollectedHighScore = flagCollected;

            PlayerPrefs.SetInt("FlagCollectedHighScore", flagCollectedHighScore);
            PlayerPrefs.Save();

            if (!flagHighscoreisShown)
            {
                ShowNotification("HighScore");
                flagHighscoreisShown = true;
            }

        }
    }

    private void ShowNotification(string message)
    {
        
        notificationText.text = message;
        notificationText.gameObject.SetActive(true);


        StartCoroutine(HideNotificationAfterDelay(2.0f));
    }

    private IEnumerator HideNotificationAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        notificationText.gameObject.SetActive(false);
    }
}
