using UnityEngine;

public class Sounds : MonoBehaviour
{
    public AudioClip[] sounds; // Array of available sound effects

    // Cached reference to the AudioSource component on the object
    private AudioSource audioScr => GetComponent<AudioSource>();

    // Method to play a sound effect
    public void PlaySound(AudioClip clip, float volume = 1f)
    {
        // Only play sound if sound is enabled in GameManager
        if (GameManager.sound)
            audioScr.PlayOneShot(clip, volume);
    }
}