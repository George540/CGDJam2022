using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] AudioSource sfx, aliveMusic, ghostMusic;
    [SerializeField] AudioClip gameStart, openDoor, closeDoor, dropletAudio;

    public void Play(AudioClip clip)
    {
        sfx.PlayOneShot(clip);
    }

    public void GameStart()
    {
        Play(gameStart);
    }

    public void OpenDoor()
    {
        Play(openDoor);
    }

    public void CloseDoor()
    {
        Play(closeDoor);
    }

    public void DropletSound()
    {
        sfx.PlayOneShot(dropletAudio);
    }

    public IEnumerator Switch(bool isGhost)
    {
        int aliveVolume, deadVolume;

        if (isGhost)
        {
            aliveVolume = 0;
            deadVolume = 1;
        }
        else
        {
            aliveVolume = 1;
            deadVolume = 0;
        }

        while(aliveMusic.volume != aliveVolume)
        {
            aliveMusic.volume = Mathf.MoveTowards(aliveMusic.volume, aliveVolume, 0.01f);
            ghostMusic.volume = Mathf.MoveTowards(ghostMusic.volume, deadVolume, 0.01f);
            yield return new WaitForSeconds(0.003f);
        }

        aliveMusic.volume = aliveVolume;
        ghostMusic.volume = deadVolume;
    }
}
