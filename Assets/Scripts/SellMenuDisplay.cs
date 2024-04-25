using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SellMenuDisplay : MonoBehaviour
{
    public RectTransform menuUI; // Use RectTransform for UI positioning
    public Camera mainCamera; // Assign the main camera or use Camera.main
    public TMP_Text menuText; // Reference to your TMP text component

    void Update()
    {
        // Create a ray from the mouse position
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // Perform the raycast
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.CompareTag("Animal"))
            {
                menuUI.gameObject.SetActive(true);

                // Set text or other information about the animal
                menuText.text = $"You are looking at: {hit.collider.gameObject.name}";

                // Convert world position of the animal to a point on the canvas
                Vector3 screenPos = mainCamera.WorldToScreenPoint(hit.collider.transform.position);
                menuUI.position = screenPos + new Vector3(0, -50, 0); // Offset by 100 pixels on y-axis
            }
            else
            {
                menuUI.gameObject.SetActive(false);
            }
        }
        else
        {
            menuUI.gameObject.SetActive(false);
        }
    }
}
