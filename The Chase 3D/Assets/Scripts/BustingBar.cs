using System.Collections;
using System.Collections.Generic;
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

        bustingBar.fillAmount = Mathf.Lerp(bustingBar.fillAmount, bustingTimer / bustingDuration, lerpSpeed);
        //Debug.Log("Fill"+bustingBar.fillAmount);
    }
}
