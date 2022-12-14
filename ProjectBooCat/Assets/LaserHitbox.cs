using System;
using System.Collections;
using System.Collections.Generic;
using Platformer.Mechanics;
using UnityEngine;

public class LaserHitbox : MonoBehaviour
{
    [SerializeField] private Collider2D _collider2D;
    [SerializeField] private GameObject _spawnPoint;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent<PlayerController>(out var player))
        {
            GameManager.Instance._alivePlayer.transform.position = _spawnPoint.transform.position;
            GameManager.Instance.SwitchToGhostState();
            GameManager.Instance._audioManager.Play(player.laserHit);
            player.SpawnSparkleVFX();
        }
    }
}
