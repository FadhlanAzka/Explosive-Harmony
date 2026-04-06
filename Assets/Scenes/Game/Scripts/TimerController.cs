using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class TimerController : MonoBehaviour
{
    public GameplayController gameplayController;

    public GameObject ClockText;

    public TextMeshProUGUI TimerText;
    public float countdownDuration;

    public float remainingTime;
    private bool timerRunning = true;

    public AudioSource audioSource;  // Add this line

    private float tickInterval = 1f; // Ticking sound interval (1 second)
    private float nextTickTime = 0f; // Time for the next tick sound

    void Start()
    {
        remainingTime = countdownDuration;
        UpdateTimerText();
    }

    void Update()
    {
        if (timerRunning)
        {
            remainingTime -= Time.deltaTime;

            if (remainingTime < 0)
            {
                remainingTime = 0;
                TimerFinished();
            }

            UpdateTimerText();

            if (remainingTime <= 5f)
            {
                tickInterval = 0.25f;
            }

            else if (remainingTime <= 10f)
            {
                tickInterval = 0.5f; // Ticking sound interval (0.5 seconds)
            }

            else
            {
                tickInterval = 1f; // Ticking sound interval (1 second)
            }

            // Play ticking sound at regular intervals
            if (Time.time >= nextTickTime)
            {
                PlayTickSound();
                nextTickTime = Time.time + tickInterval;
            }
        }

        string currentTime = System.DateTime.Now.ToString("HH:mm");
        ClockText.GetComponent<TextMeshProUGUI>().text = currentTime;
    }

    void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(remainingTime / 60f);
        int seconds = Mathf.FloorToInt(remainingTime % 60f);

        TimerText.color = Color.red;
        TimerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void StopTimer()
    {
        timerRunning = false;
    }

    void TimerFinished()
    {
        timerRunning = false;
        gameplayController.Exploded.SetActive(true);
        gameplayController.ExplodedTimeRemaining.text = "" + remainingTime;
    }

    void PlayTickSound()
    {
        // Generate a simple beep sound
        int sampleRate = 44100;
        float frequency = 1000f;
        int sampleLength = sampleRate / 10;
        AudioClip tickClip = AudioClip.Create("Tick", sampleLength, 1, sampleRate, false);
        float[] samples = new float[sampleLength];

        for (int i = 0; i < sampleLength; i++)
        {
            samples[i] = Mathf.Sin(2 * Mathf.PI * frequency * i / sampleRate);
        }

        tickClip.SetData(samples, 0);
        audioSource.PlayOneShot(tickClip);
    }
}
