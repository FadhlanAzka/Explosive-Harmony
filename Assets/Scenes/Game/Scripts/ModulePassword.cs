using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ModulePassword : MonoBehaviour
{
    public StrikeController strikeController;

    public GameObject submitButton;
    public GameObject LED;

    public TMP_Text questionText;
    public TMP_InputField answerInput;

    public bool PasswordSuccess = false;

    private int clue;
    private int answer;

    void Start()
    {
        clue = Random.Range(1, 366);
        questionText.text = "" + clue;
        answer = CalculateDate(clue);
        answerInput.characterLimit = 4;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == submitButton)
            {
                CheckAnswer();
            }
        }
    }

    void CheckAnswer()
    {
        int userAnswer;
        bool isNumeric = int.TryParse(answerInput.text, out userAnswer);

        if (isNumeric)
        {
            if (userAnswer == answer)
            {
                Debug.Log("Correct Answer!");
                PasswordSuccess = true;
                LED.GetComponent<Renderer>().material.color = Color.green;
                answerInput.readOnly = true;
            }
            else
            {
                Debug.Log("Incorrect Answer!");
                strikeController.getStrike();
            }
        }
        else
        {
            Debug.Log("Please enter a valid number.");
        }
    }

    int CalculateDate(int dayOfYear)
    {
        int[] daysInMonth = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
        int month = 0;
        int day = dayOfYear;

        for (int i = 0; i < daysInMonth.Length; i++)
        {
            if (day > daysInMonth[i])
            {
                day -= daysInMonth[i];
                month++;
            }
            else
            {
                break;
            }
        }
       
        string monthString = (month + 1).ToString().PadLeft(2, '0');
        string dayString = day.ToString().PadLeft(2, '0');

        string fullDate = dayString + monthString;
        return int.Parse(fullDate);
    }

}

//https://nsidc.org/data/user-resources/help-center/day-year-doy-calendar