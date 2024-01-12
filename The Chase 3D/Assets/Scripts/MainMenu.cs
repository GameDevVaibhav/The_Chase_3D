using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject objectToActivate1; 
    public GameObject objectToActivate2;

    public TextMeshProUGUI cashHighscore;
    public TextMeshProUGUI carDestroyedHighscore;
    public TextMeshProUGUI flagCollectedHighscore;

    // Start is called before the first frame update
    void Start()
    {
        int cash = PlayerPrefs.GetInt("CashHighscore", 0);
        int car = PlayerPrefs.GetInt("CarDestroyedHighScore", 0);
        int flag= PlayerPrefs.GetInt("FlagCollectedHighScore", 0);

        cashHighscore.text= cash.ToString();
        carDestroyedHighscore.text= car.ToString();
        flagCollectedHighscore.text= flag.ToString();

        // Assuming you have two buttons as direct children of the Canvas
        Button[] buttons = GetComponentsInChildren<Button>();
        foreach (Button button in buttons)
        {
            // Store the original scale of each button
            ButtonHoverEffect hoverEffect = button.gameObject.AddComponent<ButtonHoverEffect>();
            hoverEffect.originalScale = button.transform.localScale;

            // Assign the correct objectToActivate reference based on the button
            if (button.name == "City")
            {
                hoverEffect.objectToActivate = objectToActivate1;
            }
            else if (button.name == "Mud")
            {
                hoverEffect.objectToActivate = objectToActivate2;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartCityMap()
    {
        SceneManager.LoadScene(1);
    }

    public void StartMudMap()
    {
        SceneManager.LoadScene(2);
    }
}

public class ButtonHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Vector3 originalScale;
    public float popScaleFactor = 1.2f; // Adjust this value to control the pop-up effect
    public float scaleSpeed = 5f; // Adjust this value to control the speed of scaling

    public GameObject objectToActivate; // Reference to the GameObject you want to activate

    private bool isHovered = false;

    private void Update()
    {
        // Lerp the scale toward the target scale
        float lerpFactor = scaleSpeed * Time.deltaTime;
        transform.localScale = Vector3.Lerp(transform.localScale, isHovered ? originalScale * popScaleFactor : originalScale, lerpFactor);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovered = true;
        ActivateObject(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovered = false;
        ActivateObject(false);
    }

    private void ActivateObject(bool activate)
    {
        if (objectToActivate != null)
        {
            objectToActivate.SetActive(activate);
        }
    }
}