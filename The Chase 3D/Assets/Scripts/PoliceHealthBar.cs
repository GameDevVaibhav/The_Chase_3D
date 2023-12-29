using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PoliceHealthBar : MonoBehaviour
{
    
    public Image healthBar;
    private Camera camera;

    private void Start()
    {
        camera= Camera.main;
    }

    // Update is called once per frame
    private void Update()
    {

        transform.rotation = Quaternion.LookRotation(transform.position - camera.transform.position);
    }
    public void  UpdatePoliceHealthBar(float maxHealth,float currentHealth)
    {
       healthBar.fillAmount = currentHealth/maxHealth;
    }
}
