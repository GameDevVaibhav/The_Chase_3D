using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    public Image healthBar;
    float lerpSpeed;

    private void Update()
    {
        lerpSpeed= 3f*Time.deltaTime;
    }
    public void UpdateHealthBar(float maxHealth, float currentHealth)
    {
        healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, currentHealth/maxHealth, lerpSpeed);
        Color healthColor = Color.Lerp(Color.red, Color.green, (currentHealth / maxHealth));
        healthBar.color = healthColor;  
    }
    
}
