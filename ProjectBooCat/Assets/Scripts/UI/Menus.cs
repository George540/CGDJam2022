using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Menus : MonoBehaviour
{
    GameObject eventSystem;
    [SerializeField] GameObject mainMenu, optionsMenu, playButton, volumeSlider;

    private void Start()
    {
        eventSystem = GameObject.Find("EventSystem");
    }

    public void Play()
    {
        Debug.Log("Play now!");
    }

    public void Options()
    {
        mainMenu.SetActive(false);
        optionsMenu.SetActive(true);
        eventSystem.GetComponent<EventSystem>().SetSelectedGameObject(volumeSlider);
    }

    public void Back()
    {
        optionsMenu.SetActive(false);
        mainMenu.SetActive(true);
        eventSystem.GetComponent<EventSystem>().SetSelectedGameObject(playButton);
    }
}
