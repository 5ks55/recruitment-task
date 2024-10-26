using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : Sounds
{
    [SerializeField] private GameObject music; // Reference to the music button GameObject
    [SerializeField] private GameObject sound; // Reference to the sound button GameObject
    [SerializeField] private AudioSource musicAudio; // Reference to the AudioSource for music

    private void Start()
    {
        // Set the initial state of the music button and audio based on GameManager settings
        if (GameManager.music)
        {
            music.GetComponent<Image>().color = Color.white;
            musicAudio.mute = false;
        }
        else
        {
            music.GetComponent<Image>().color = Color.red;
            musicAudio.mute = true;
        }

        // Set the initial state of the sound button based on GameManager settings
        sound.GetComponent<Image>().color = GameManager.sound ? Color.white : Color.red;
    }

    // Toggles music settings, updating GameManager and PlayerPrefs
    public void Music()
    {
        if (GameManager.music)
        {
            PlayerPrefs.SetInt("music", 0);
            GameManager.music = false;
            music.GetComponent<Image>().color = Color.red;
            musicAudio.mute = true;
        }
        else
        {
            PlayerPrefs.SetInt("music", 1);
            GameManager.music = true;
            music.GetComponent<Image>().color = Color.white;
            musicAudio.mute = false;
        }
    }

    // Toggles sound settings, updating GameManager and PlayerPrefs
    public void Sound()
    {
        if (GameManager.sound)
        {
            PlayerPrefs.SetInt("sound", 0);
            GameManager.sound = false;
            sound.GetComponent<Image>().color = Color.red;
        }
        else
        {
            PlayerPrefs.SetInt("sound", 1);
            GameManager.sound = true;
            sound.GetComponent<Image>().color = Color.white;
        }
    }

    // Controls the visibility of the music and sound buttons
    public void ShowHideButtons(bool active)
    {
        music.SetActive(active);
        sound.SetActive(active);
    }

    // Plays a click sound effect using inherited method from Sounds class
    public void ClickSound()
    {
        PlaySound(sounds[0]);
    }
}