using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI descriptionText; // Reference to the UI Text element

    private float timeElapsed; // Time elapsed since the game started
    private bool isTimerRunning = true; // Flag to control the timer's state

    private void Update()
    {
        // If the timer is active, increase the time
        if (isTimerRunning)
        {
            timeElapsed += Time.deltaTime;
            UpdateTimerDisplay();
        }
    }

    // Method to update the displayed time
    private void UpdateTimerDisplay()
    {
        // Convert elapsed time to minutes, seconds, and milliseconds
        int minutes = Mathf.FloorToInt(timeElapsed / 60f);
        int seconds = Mathf.FloorToInt(timeElapsed % 60);
        int milliseconds = Mathf.FloorToInt((timeElapsed * 100f) % 100);

        // Display time in the required format 00:00,00
        descriptionText.text = $"Time: {minutes:00}:{seconds:00},{milliseconds:00}";
    }

    // Method to reset the timer 
    public void ResetTimer()
    {
        timeElapsed = 0f;
        isTimerRunning = false;
        UpdateTimerDisplay();
    }

    // Method to stop the timer
    public void StopTimer()
    {
        isTimerRunning = false;
    }

    // Method to start the timer
    public void StartTimer()
    {
        timeElapsed = 0f;
        isTimerRunning = true;
    }

    // Method to handle the end of the game
    public void EndGame()
    {
        StopTimer();
        FindObjectOfType<GameManager>().ShowEndGameScreen(timeElapsed);
    }

    // Hide the timer display 
    public void HideTimerDisplay()
    {
        descriptionText.gameObject.SetActive(false);
    }

    // Show the timer display
    public void ShowTimerDisplay()
    {
        descriptionText.gameObject.SetActive(true);
    }
}