using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{

    private AudioSource audio;
    private float volume = 1f;

    void Start()
    {
        volume = volume = PlayerPrefs.GetFloat("volume");
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        audio.volume = volume;
    }

    // Method that is called by slider game object
    // This method takes vol value passed by slider
    // and sets it as musicValue
    public void SetVolume(float vol)
    {
        volume = vol;
    }

    void OnDisable()
    {
        PlayerPrefs.SetFloat("volume", volume);
    }
}
