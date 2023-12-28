using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CashBox : MonoBehaviour
{
    public GameObject dollarBillParticles; // Reference to the dollar bill particle system
    public Material cashBoxMaterial; // Reference to the material of the CashBox
    public float emissionIncreaseRate = 0.5f; // Adjust the rate at which emission intensity increases

    private int hitCount = 0;
    private Score scoreManager;

    void Start()
    {
        // Find the Score script or adjust accordingly based on your setup
        scoreManager = FindObjectOfType<Score>();

        // Ensure that a material is assigned to the CashBox
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

                // Assuming your emission color is green, you can modify the green channel
                float newIntensity = currentEmissionColor.g + hitCount * emissionIncreaseRate;

                // Make sure the intensity does not exceed 1
                newIntensity = Mathf.Clamp01(newIntensity);

                Color newEmissionColor = new Color(currentEmissionColor.r, newIntensity, currentEmissionColor.b);
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
        // Increase cash score by 100 when the CashBox is destroyed
        scoreManager.IncreaseCashScore(100);

        // Trigger the particle system for the dollar bill explosion
        Instantiate(dollarBillParticles, transform.position, Quaternion.identity);

        // Play the cash box destroyed sound
        AudioManager.Instance.PlayCashBoxDestroyedSound();

        Debug.Log("CashBox Destroyed!");
        Destroy(gameObject);
    }
}
