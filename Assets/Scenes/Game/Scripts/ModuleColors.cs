using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleColors : MonoBehaviour
{
    public StrikeController strikeController;

    public GameObject Blue;
    public GameObject Yellow;
    public GameObject Green;
    public GameObject Red;
    public GameObject LED;

    // Original colors
    private Color blueOriginalColor;
    private Color yellowOriginalColor;
    private Color greenOriginalColor;
    private Color redOriginalColor;

    private GameObject lastSelectedLight;

    public bool ColourSuccess = false;

    public int correctClickCount = 0;    

    void Start()
    {
        blueOriginalColor = Blue.GetComponent<Renderer>().material.color;
        yellowOriginalColor = Yellow.GetComponent<Renderer>().material.color;
        greenOriginalColor = Green.GetComponent<Renderer>().material.color;
        redOriginalColor = Red.GetComponent<Renderer>().material.color;

        StartCoroutine(ChangeColorsRandomly());
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                GameObject clickedLight = hit.collider.gameObject;
                OnLightClicked(clickedLight);
            }
        }

        if (correctClickCount == 5) // Jika sudah 5 klik yang benar
        {           
            StopCoroutine(ChangeColorsRandomly());

            LED.GetComponent<Renderer>().material.color = Color.green;
            Blue.GetComponent<Renderer>().material.color = Color.blue;            
            Yellow.GetComponent<Renderer>().material.color = Color.yellow;
            Green.GetComponent<Renderer>().material.color = Color.green;
            Red.GetComponent<Renderer>().material.color = Color.red;

            Blue.GetComponent<Collider>().enabled = false;
            Yellow.GetComponent<Collider>().enabled = false;
            Green.GetComponent<Collider>().enabled = false;
            Red.GetComponent<Collider>().enabled = false;

            ColourSuccess = true;
        }
    }

    private void OnLightClicked(GameObject clickedLight)
    {
        if (clickedLight == Blue || clickedLight == Yellow || clickedLight == Green || clickedLight == Red)
        {
            if (clickedLight == lastSelectedLight)
            {
                correctClickCount++;
                StartCoroutine(ChangeColorsRandomly());
            }
            else
            {
                strikeController.getStrike();
            }
        }
    }

    IEnumerator ChangeColorsRandomly()
    {
        SetLightsInteractable(false);

        yield return new WaitForSeconds(Random.Range(1f, 5f)); // Random delay between changes

        int randomLight = Random.Range(0, 4);
        GameObject selectedLight = null;
        Color newColor = Color.black;

        switch (randomLight)
        {
            case 0:
                selectedLight = Blue;
                newColor = new Color(0, 0, 1); // Blue (0, 0, 255) in normalized color
                break;
            case 1:
                selectedLight = Yellow;
                newColor = new Color(1, 1, 0); // Yellow (255, 255, 0) in normalized color
                break;
            case 2:
                selectedLight = Green;
                newColor = new Color(0, 1, 0); // Green (0, 255, 0) in normalized color
                break;
            case 3:
                selectedLight = Red;
                newColor = new Color(1, 0, 0); // Red (255, 0, 0) in normalized color
                break;
        }

        if (selectedLight != null)
        {
            Renderer lightRenderer = selectedLight.GetComponent<Renderer>();
            Color originalColor = lightRenderer.material.color;

            lightRenderer.material.color = newColor;
            lastSelectedLight = selectedLight; // Store the selected light
            yield return new WaitForSeconds(1f); // Light stays on for 1 second
            lightRenderer.material.color = originalColor; // Revert to original color

            SetLightsInteractable(true);
        }
    }

    private void SetLightsInteractable(bool interactable)
    {
        Blue.GetComponent<Collider>().enabled = interactable;
        Yellow.GetComponent<Collider>().enabled = interactable;
        Green.GetComponent<Collider>().enabled = interactable;
        Red.GetComponent<Collider>().enabled = interactable;
    }
}