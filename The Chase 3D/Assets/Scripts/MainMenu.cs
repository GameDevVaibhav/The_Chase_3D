using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    private Vector3 originalButtonScale;
    public float popScaleFactor = 1.2f; // Adjust this value to control the pop-up effect

    // Start is called before the first frame update
    void Start()
    {
        // Assuming you have two buttons as direct children of the Canvas
        Button[] buttons = GetComponentsInChildren<Button>();
        foreach (Button button in buttons)
        {
            // Store the original scale of each button
            button.gameObject.AddComponent<ButtonHoverEffect>().originalScale = button.transform.localScale;
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
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovered = false;
    }
}
