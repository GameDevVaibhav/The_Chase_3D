using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CountDown : MonoBehaviour
{
    public TextMeshProUGUI countdownText;
    public GameObject objectActivation;
    public PlayerCarController playerCarController;

    void Start()
    {
        StartCoroutine(CountdownCoroutine());
    }

    IEnumerator CountdownCoroutine()
    {
        yield return StartCoroutine(Countdown(3));
        countdownText.text = "Go!";
        yield return new WaitForSeconds(0.5f);
        objectActivation.SetActive(true);
        playerCarController.ActivateController();
        countdownText.gameObject.SetActive(false);

       
    }

    IEnumerator Countdown(int seconds)
    {
        int countdownValue = seconds;
        while (countdownValue > 0)
        {
            countdownText.text = countdownValue.ToString();
            yield return new WaitForSeconds(0.8f);
            countdownValue--;
        }
    }
}
