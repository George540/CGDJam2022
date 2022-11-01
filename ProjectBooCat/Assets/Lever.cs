using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Lever : MonoBehaviour
{
    [SerializeField] private GameObject[] _lasersSet1;

    [SerializeField] private GameObject[] _lasersSet2;

    public List<Transform> _laserBeamsSet1;
    public List<Transform> _laserBeamsSet2;

    private bool leverOnSwitch;

    private void Start()
    {
        leverOnSwitch = false;
        
        foreach(var laser in _lasersSet1)
            _laserBeamsSet1.Add(laser.transform.GetChild(0));
        
        foreach(var laser in _lasersSet2)
            _laserBeamsSet2.Add(laser.transform.GetChild(0));
        
        // By default, set 2 laser beams should be disabled on start
        TurnOnSet1();
    }

    // Turn on set 1, turn off set 2
    private void TurnOnSet1()
    {
        // Change sprite of laser generator
        // Change sprite of lever in the ActivateLever function
        foreach(var laser in _laserBeamsSet1)
            laser.gameObject.SetActive(true);
        
        
        
        
        foreach(var laser in _laserBeamsSet2)
            laser.gameObject.SetActive(false);
        leverOnSwitch = false;
        
        Debug.Log("TurnOnSet1()");
    }

    // Turn off set 1, turn on set 2
    private void TurnOnSet2()
    {
        foreach(var laser in _laserBeamsSet1)
            laser.gameObject.SetActive(false);
        foreach(var laser in _laserBeamsSet2)
            laser.gameObject.SetActive(true);
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
        GameManager.Instance.canPressLever = true;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (GameManager.Instance.playerPressLever)
        {
            ActivateLever();
            GameManager.Instance.playerPressLever = false;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        GameManager.Instance.canPressLever = false;
    }
}
