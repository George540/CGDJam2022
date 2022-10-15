using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTrap : MonoBehaviour
{
    [SerializeField] private LineRenderer Laser;
    
    private void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up);

        if (hit.collider != null)
        {
            var hitPosition = hit.transform.position;
            
            Laser.SetPosition(1, hitPosition);
        }
    }
}
