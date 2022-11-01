using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    [SerializeField] private GameObject[] _lasersSet1;

    [SerializeField] private GameObject[] _lasersSet2;

    private List<Transform> _laserBeamsSet1;
    private List<Transform> _laserBeamsSet2;

    private bool isLeverOn;

    private void Start()
    {
        isLeverOn = false;
        
        foreach(var laser in _lasersSet1)
            _laserBeamsSet1.Add(laser.transform.GetChild(0));
        
        foreach(var laser in _lasersSet2)
            _laserBeamsSet2.Add(laser.transform.GetChild(0));
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
    }

    // Turn off set 1, turn on set 2
    private void TurnOnSet2()
    {
        foreach(var laser in _laserBeamsSet1)
            laser.gameObject.SetActive(false);
        foreach(var laser in _laserBeamsSet2)
            laser.gameObject.SetActive(true);
    }

    public void ActivateLever()
    {
        switch (isLeverOn)
        {
            case true:
                TurnOnSet1();
                break;
            default:
                TurnOnSet2();
                break;
        }
    }
}
