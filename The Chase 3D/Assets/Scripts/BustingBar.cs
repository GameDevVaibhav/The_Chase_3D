using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BustingBar : MonoBehaviour
{
    public Image bustingBar;
    float lerpSpeed;

    

    private void Update()
    {
        lerpSpeed = 3f * Time.deltaTime;
    }

    public void UpdateBustingBar(float bustingTimer, float bustingDuration)
    {
        float fillAmount = Mathf.Lerp(bustingBar.fillAmount, bustingTimer / bustingDuration, lerpSpeed);
        bustingBar.fillAmount = fillAmount;

        // Adjust the alpha using Lerp for a smooth fade effect
        float targetAlpha = fillAmount > 0 ? 1f : 0f;
        float currentAlpha = bustingBar.color.a;
        float newAlpha = Mathf.Lerp(currentAlpha, targetAlpha, lerpSpeed);

        SetBarAlpha(newAlpha);
        
    }

    private void SetBarAlpha(float alpha)
    {
        Color barColor = bustingBar.color;
        barColor.a = alpha;
        bustingBar.color = barColor;
    }
}
