using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/* Here there are 2 buttons City and Mud which are map and on click they will start that particular Map. And Hover over these buttons it activates object that 
    object is a videoplayer object and plays the video of that map.
 */
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

        
        Button[] buttons = GetComponentsInChildren<Button>();
        foreach (Button button in buttons)
        {
            
            ButtonHoverEffect hoverEffect = button.gameObject.AddComponent<ButtonHoverEffect>();
            hoverEffect.originalScale = button.transform.localScale;

            
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
    public void Quit()
    {
        Application.Quit();
    }
}

public class ButtonHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Vector3 originalScale;
    public float popScaleFactor = 1.2f; 
    public float scaleSpeed = 5f; 

    public GameObject objectToActivate; 

    private bool isHovered = false;

    private void Start()
    {
        
    }

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