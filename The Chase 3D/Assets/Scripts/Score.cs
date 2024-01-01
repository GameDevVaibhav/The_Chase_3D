using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    public TextMeshProUGUI cashText;
    public TextMeshProUGUI carDestroyedText;
    private int cashScore = 0;
    private int carDestroyed = 0;

    void Start()
    {
       
       

        // Update the initial score display
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        // Update the TextMeshProUGUI component with the current cash score
        cashText.text = cashScore.ToString();
        carDestroyedText.text = carDestroyed.ToString();
    }

    public void IncreaseCashScore(int amount)
    {
        // Increase the cash score by the specified amount
        cashScore += amount;

        // Update the score display
        UpdateScoreText();
    }
    public void IncreaseCarDestroyed(int count)
    {
        carDestroyed+= count;
        UpdateScoreText();

    }
}
