using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioSource audioSource;

    void Update()
    {
        // Toggle play/pause when the m key is pressed
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (audioSource.isPlaying)
            {
                audioSource.Pause();  // Pause the audio if it is playing
            }
            else
            {
                audioSource.Play();  // Play the audio if it is paused
            }
        }
    }
}
