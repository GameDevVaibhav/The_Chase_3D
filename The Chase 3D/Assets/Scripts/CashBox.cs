using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CashBox : MonoBehaviour
{
    private int hitCount = 0;
    private Score scoreManager;

    void Start()
    {
        // Find the Score script or adjust accordingly based on your setup
        scoreManager = FindObjectOfType<Score>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Shot"))
        {
            hitCount++;

            Debug.Log("CashBox Hit! Hit Count: " + hitCount);

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

        // Perform any additional actions before destroying the CashBox, if needed
        Debug.Log("CashBox Destroyed!");
        Destroy(gameObject);
    }
}
