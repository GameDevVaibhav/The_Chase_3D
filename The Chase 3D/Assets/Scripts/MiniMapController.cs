using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniMapController : MonoBehaviour
{
    public Camera minimapCamera;  // Assign your minimap camera in the Unity Editor
    public RenderTexture minimapTexture;  // Assign your minimap render texture in the Unity Editor
    public RawImage minimapRawImage;  // Assign your RawImage in the Unity Editor

    void Update()
    {
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Health"))
        {
            // Calculate screen position
            Vector3 screenPos = minimapCamera.WorldToScreenPoint(obj.transform.position);

            // Check if the object is within the camera's view
            if (screenPos.x >= 0 && screenPos.x <= Screen.width &&
                screenPos.y >= 0 && screenPos.y <= Screen.height)
            {
                // Object is within the camera's view, no need to clamp
                continue;
            }

            // Clamp the object at the edge of the minimap
            screenPos.x = Mathf.Clamp(screenPos.x, 0, Screen.width);
            screenPos.y = Mathf.Clamp(screenPos.y, 0, Screen.height);

            // Convert clamped position to texture coordinates
            Vector2 textureCoords = new Vector2(screenPos.x / Screen.width, screenPos.y / Screen.height);

            // Debug information
            Debug.Log($"Object {obj.name} outside camera view. Clamped position: {screenPos}, Texture Coordinates: {textureCoords}");

            // Update the minimap icon's position (assuming it's a RawImage)
            UpdateIconPosition(obj, textureCoords);
        }
    }

    void UpdateIconPosition(GameObject obj, Vector2 textureCoords)
    {
        // Assuming your RawImage is used for rendering the minimap
        if (minimapRawImage != null)
        {
            // Update the UV rectangle of the RawImage to display the object on the minimap
            Rect uvRect = minimapRawImage.uvRect;
            uvRect.position = textureCoords;
            minimapRawImage.uvRect = uvRect;
        }
    }
}
