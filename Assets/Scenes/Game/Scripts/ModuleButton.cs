using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ModuleButton : MonoBehaviour
{
    public StrikeController strikeController;

    public GameObject Button;
    public GameObject LED;

    public Material PlayMaterial;

    public TimerController timerController;

    public Color buttonColor;
    public Color[] colorOptions = { Color.red, Color.blue, Color.yellow, Color.white, Color.black };

    public bool ButtonSuccess = false;
    public bool isHoldingButton = false;

    public float buttonHoldStartTime;

    void Start()
    {
        Renderer renderer = Button.GetComponent<Renderer>();

        if (renderer != null)
        {
            renderer.material = PlayMaterial;

            int randomIndex = Random.Range(0, colorOptions.Length);
            buttonColor = colorOptions[randomIndex];
            renderer.material.color = buttonColor;
        }
        else
        {
            Debug.LogError("Renderer component not found on the target GameObject.");
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == Button)
            {
                isHoldingButton = true;
                buttonHoldStartTime = Time.time;
            }
        }

        if (Input.GetMouseButtonUp(0) && isHoldingButton)
        {
            isHoldingButton = false;
            float holdDuration = Time.time - buttonHoldStartTime;

            if (CheckButtonReleaseCondition(holdDuration))
            {
                ButtonSuccess = true;
                LED.GetComponent<Renderer>().material.color = Color.green;
                CapsuleCollider capsuleCollider = Button.GetComponent<CapsuleCollider>();
                capsuleCollider.enabled = false;
            }
            else
            {
                strikeController.getStrike();
            }
        }
    }

    bool CheckButtonReleaseCondition(float holdDuration)
    {
        TimerController timerController = FindObjectOfType<TimerController>();
        int remainingSeconds = Mathf.FloorToInt(timerController.remainingTime % 60);

        if (buttonColor == Color.black && holdDuration >= 5f)
        {
            return true;
        }
        else if (buttonColor == Color.white && ContainsDigit(remainingSeconds, 1))
        {
            return true;
        }
        else if (buttonColor == Color.red && holdDuration < 1f)
        {
            return true;
        }
        else if (buttonColor == Color.blue && ContainsDigit(remainingSeconds, 4))
        {
            return true;
        }
        else if (buttonColor == Color.yellow && ContainsDigit(remainingSeconds, 6))
        {
            return true;
        }

        return false;
    }

    bool ContainsDigit(int number, int digit)
    {
        while (number > 0)
        {
            if (number % 10 == digit)
            {
                return true;
            }
            number /= 10;
        }
        return false;
    }
}