using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] AudioSource gameStart, select, confirm;

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }

    public void GameStart()
    {
        gameStart.Play();
    }

    public void PlaySelect()
    {
        select.Play();
    }

    public void PlayConfirm()
    {
        confirm.Play();
    }
}
