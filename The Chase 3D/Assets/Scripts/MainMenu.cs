using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject objectToActivate1; // Reference to the first GameObject you want to activate
    public GameObject objectToActivate2; // Reference to the second GameObject you want to activate

    // Start is called before the first frame update
    void Start()
    {
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