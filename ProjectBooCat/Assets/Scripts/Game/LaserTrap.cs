using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LaserTrap : MonoBehaviour
{
    [SerializeField] private LineRenderer Laser;
    [SerializeField] private EdgeCollider2D Hitbox;
    private List<Vector2> Points;
    private enum HitState
    {
        Hit, NotHit
    }
    
    private HitState CurrentState;

    private void Awake()
    {
        Points = new List<Vector2>();
        Points.Add(Laser.GetPosition(0));
        CurrentState = HitState.NotHit;
    }

    private void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up);
        if (CurrentState == HitState.NotHit)
        {
            if (hit.collider != null)
            {
                var hitPosition = hit.transform.position;
                CurrentState = HitState.Hit;
                Laser.SetPosition(1, hitPosition);
                Points.Add(Laser.GetPosition(1));
                var allPoints = Points.ToArray();
                Hitbox.points = allPoints;
            }
        }
    }
}
