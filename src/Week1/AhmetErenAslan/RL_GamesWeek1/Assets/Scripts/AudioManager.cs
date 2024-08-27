using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    [SerializeField] AudioSource audioSource;


    void Start()
    {
        audioSource.Play();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.M)) {
            if (!audioSource.isPlaying) {
                audioSource.Play();
            } else { 
                audioSource.Pause();
            }
        }
    }
}
