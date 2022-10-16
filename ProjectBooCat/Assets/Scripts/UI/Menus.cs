using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Menus : MonoBehaviour
{
    [SerializeField] GameObject mainMenu, optionsMenu;
    [SerializeField] AudioMixer audioMixer;

    public void Play()
    {
        Debug.Log("Play now!");
    }

    public void Options()
    {
        mainMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }

    public void Back()
    {
        optionsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void setVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }
}
