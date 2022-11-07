using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Lever : MonoBehaviour
{
    [SerializeField] private GameObject[] _lasersSet1;

    [SerializeField] private GameObject[] _lasersSet2;
    
    [SerializeField] public Sprite _laserMachineOnSprite;
    [SerializeField] public Sprite _laserMachineOffSprite;

    public List<Transform> _laserBeamsSet1;
    public List<Transform> _laserBeamsSet2;
    
    public Animator _leverAnimator;

    private bool leverOnSwitch;
    private bool isStandingNearLever;

    private void Start()
    {
        leverOnSwitch = false;
        _laserBeamsSet1 = new List<Transform>(_lasersSet1.Length);
        _laserBeamsSet2 = new List<Transform>(_lasersSet2.Length);
        
        foreach(var laser in _lasersSet1)
            _laserBeamsSet1.Add(laser.transform.GetChild(0));
        
        foreach(var laser in _lasersSet2)
            _laserBeamsSet2.Add(laser.transform.GetChild(0));
        
        // By default, set 2 laser beams should be disabled on start
        // TurnOnSet1();
    }

    // Turn on set 1, turn off set 2
    private void TurnOnSet1()
    {
        // Change sprite of laser generator
        _leverAnimator.Play("LeverTurningOn");
        foreach(var laser in _laserBeamsSet1)
            laser.gameObject.SetActive(true);
        
        foreach(var laser in _laserBeamsSet2)
            laser.gameObject.SetActive(false);

        foreach (var laser in _lasersSet1)
        {
            laser.GetComponent<Animator>().enabled = true;
            laser.GetComponent<SpriteRenderer>().sprite = _laserMachineOnSprite;
        }

        foreach (var laser in _lasersSet2)
        {
            laser.GetComponent<Animator>().enabled = false;
            laser.GetComponent<SpriteRenderer>().sprite = _laserMachineOffSprite;
        }

        leverOnSwitch = false;

        Debug.Log("TurnOnSet1()");
    }

    // Turn off set 1, turn on set 2
    private void TurnOnSet2()
    {
        _leverAnimator.Play("LeverTurningOff");
        foreach(var laser in _laserBeamsSet1)
            laser.gameObject.SetActive(false);
        foreach(var laser in _laserBeamsSet2)
            laser.gameObject.SetActive(true);

        foreach (var laser in _lasersSet1)
        {
            laser.GetComponent<Animator>().enabled = false;
            laser.GetComponent<SpriteRenderer>().sprite = _laserMachineOffSprite;
            if (laser.GetComponentInChildren<AudioSource>() && laser.GetComponentInChildren<AudioSource>().isPlaying)
            {
                laser.gameObject.GetComponentInChildren<AudioSource>().Stop();
            }
        }

        foreach (var laser in _lasersSet2)
        {
            laser.GetComponent<Animator>().enabled = true;
            laser.GetComponent<SpriteRenderer>().sprite = _laserMachineOnSprite;
            if (laser.GetComponentInChildren<AudioSource>() && !laser.GetComponentInChildren<AudioSource>().isPlaying)
            {
                laser.gameObject.GetComponentInChildren<AudioSource>().Play();
            }
        }

        leverOnSwitch = true;

        Debug.Log("TurnOnSet2()");
    }

    public void ActivateLever()
    {
        switch (leverOnSwitch)
        {
            case true:
                TurnOnSet1();
                break;
            default:
                TurnOnSet2();
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.canPressLever = true;
            isStandingNearLever = true;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (GameManager.Instance.playerPressLever && isStandingNearLever)
        {
            ActivateLever();
            GameManager.Instance.playerPressLever = false;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.canPressLever = false;
            isStandingNearLever = false;
        }
    }

    public void ActivateLaserSound()
    {
        foreach (var laser in _laserBeamsSet1)
        {
            if (laser.gameObject.activeSelf && laser.GetComponentInChildren<AudioSource>() && !laser.GetComponentInChildren<AudioSource>().isPlaying)
            {
                laser.gameObject.GetComponentInChildren<AudioSource>().Play();
            }
        }

        foreach (var laser in _laserBeamsSet2)
        {
            if (laser.gameObject.activeSelf && laser.GetComponentInChildren<AudioSource>() && !laser.GetComponentInChildren<AudioSource>().isPlaying)
            {
                laser.gameObject.GetComponentInChildren<AudioSource>().Play();
            }
        }
    }
    
    public void DeactivateLaserSound()
    {
        foreach (var laser in _laserBeamsSet1)
        {
            if (laser.gameObject.activeSelf && laser.GetComponentInChildren<AudioSource>() && laser.GetComponentInChildren<AudioSource>().isPlaying)
            {
                laser.gameObject.GetComponentInChildren<AudioSource>().Stop();
            }
        }

        foreach (var laser in _laserBeamsSet2)
        {
            if (laser.gameObject.activeSelf && laser.GetComponentInChildren<AudioSource>() && laser.GetComponentInChildren<AudioSource>().isPlaying)
            {
                laser.gameObject.GetComponentInChildren<AudioSource>().Stop();
            }
        }
    }
}
