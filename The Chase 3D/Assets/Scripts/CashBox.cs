using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*  This is attached to Cashbox. it checks the collision with shots and increase its emission intensity with every hit and after some shots it is destroyed and
    cashscore is increased.
 */

public class CashBox : MonoBehaviour
{
    public GameObject dollarBillParticles; 
    public Material cashBoxMaterial; 
    public float emissionIncreaseRate = 0.2f; 

    private int hitCount = 0;
    private Score scoreManager;

    void Start()
    {
        
        scoreManager = FindObjectOfType<Score>();

        
        if (cashBoxMaterial == null)
        {
            Renderer renderer = GetComponent<Renderer>();
            if (renderer != null)
            {
                cashBoxMaterial = renderer.material;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Shot"))
        {
            hitCount++;

            Debug.Log("CashBox Hit! Hit Count: " + hitCount);

            // Increase emission intensity with every hit
            if (cashBoxMaterial != null)
            {
                Color currentEmissionColor = cashBoxMaterial.GetColor("_EmissionColor");

                
                float newIntensity = currentEmissionColor.g + hitCount * emissionIncreaseRate;

                
                float gammaSpaceIntensity = Mathf.LinearToGammaSpace(newIntensity);

                

                Color newEmissionColor = new Color(currentEmissionColor.r, gammaSpaceIntensity, currentEmissionColor.b);

                
                cashBoxMaterial.SetColor("_EmissionColor", newEmissionColor);
            }

            if (hitCount >= 5)
            {
                DestroyCashBox();
            }
        }
    }

    void DestroyCashBox()
    {
       
        scoreManager.IncreaseCashScore(100);

       
        Instantiate(dollarBillParticles, transform.position, Quaternion.identity);

        
        AudioManager.Instance.PlayCashBoxDestroyedSound();

        Debug.Log("CashBox Destroyed!");
        Destroy(gameObject);
    }
}
