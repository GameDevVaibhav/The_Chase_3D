using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverUi;
    public GameObject playUi;
    public void HandleGameOver()
    {
        playUi.SetActive(false);

        gameOverUi.SetActive(true);
    }
}
