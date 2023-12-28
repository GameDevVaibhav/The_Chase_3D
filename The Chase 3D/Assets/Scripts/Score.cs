using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    public TextMeshProUGUI cashText;
    private int cashScore = 0;

    void Start()
    {
       
       

        // Update the initial score display
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        // Update the TextMeshProUGUI component with the current cash score
        cashText.text = cashScore.ToString();
    }

    public void IncreaseCashScore(int amount)
    {
        // Increase the cash score by the specified amount
        cashScore += amount;

        // Update the score display
        UpdateScoreText();
    }
}
