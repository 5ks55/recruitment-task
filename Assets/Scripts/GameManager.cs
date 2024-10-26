using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static bool sound;
    public static bool music;

    [SerializeField] private GameObject menuWindow; // Main menu/game over window
    [SerializeField] private ButtonManager buttonManager; // Manages button visibility and interactions
    [SerializeField] private TextMeshProUGUI menuText; // Text inside the menu window
    [SerializeField] private TextMeshProUGUI buttonText; // Text on the main menu button
    [SerializeField] private Timer timer; // Timer reference
    [SerializeField] private PlayerInput playerInput; // Reference for controlling player input

    private float bestTime;
    private bool firstGame;

    private void Awake()
    {
        // Load music and sound settings from PlayerPrefs
        music = PlayerPrefs.GetInt("music", 1) == 1;
        sound = PlayerPrefs.GetInt("sound", 1) == 1;
    }

    private void Start()
    {
        // Load the best time and check if it's the first game session
        bestTime = PlayerPrefs.GetFloat("BestTime", float.MaxValue);
        firstGame = PlayerPrefs.GetInt("FirstGameFlag", 1) != 0;

        if (firstGame)
            ShowMainMenu();
        else
        {
            buttonManager.ClickSound();
            StartGame();
        }
    }

    // Displays the main menu screen
    public void ShowMainMenu()
    {
        menuWindow.SetActive(true);
        buttonManager.ShowHideButtons(true);
        playerInput.enabled = false; // Disable player input in menu
        timer.HideTimerDisplay();

        // Display the best time or a default value if no best time is set
        menuText.text = $"Best time: {FormatTime(bestTime == float.MaxValue ? 0f : bestTime)}";
        buttonText.text = "Start";

        timer.StopTimer();
    }

    // Starts the game, initializing or restarting elements as needed
    public void StartGame()
    {
        if (buttonText.text == "Start")
        {
            PlayerPrefs.SetInt("FirstGameFlag", 1);
            menuWindow.SetActive(false);
            buttonManager.ShowHideButtons(false);
            playerInput.enabled = true; // Enable player input
            timer.ShowTimerDisplay();
            timer.ResetTimer();
            timer.StartTimer();
        }
        else if (buttonText.text == "Try again")
        {
            PlayerPrefs.SetInt("FirstGameFlag", 0);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload the scene
        }
    }

    // Shows the game over screen with current and best time
    public void ShowEndGameScreen(float currentTime)
    {
        menuWindow.SetActive(true);
        buttonManager.ShowHideButtons(true);
        playerInput.enabled = false;
        timer.HideTimerDisplay();

        // Update best time if the current time is a new record
        if (currentTime < bestTime)
        {
            bestTime = currentTime;
            PlayerPrefs.SetFloat("BestTime", bestTime);
        }

        menuText.text = $"Current time: {FormatTime(currentTime)}\nBest time: {FormatTime(bestTime)}";
        buttonText.text = "Try again";

        timer.StopTimer();
    }

    // Formats time as mm:ss,ms for display purposes
    private string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60);
        int milliseconds = Mathf.FloorToInt((time * 100f) % 100);
        return $"{minutes:00}:{seconds:00},{milliseconds:00}";
    }
}