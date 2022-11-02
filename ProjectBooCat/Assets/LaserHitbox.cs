using System;
using System.Collections;
using System.Collections.Generic;
using Platformer.Mechanics;
using UnityEngine;

public class LaserHitbox : MonoBehaviour
{
    [SerializeField] private Collider2D _collider2D;

    private void Update()
    {
        _collider2D.enabled = GameManager.Instance.IsGhost ? false : true;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        GameManager.Instance.SwitchToGhostState();
    }
}
