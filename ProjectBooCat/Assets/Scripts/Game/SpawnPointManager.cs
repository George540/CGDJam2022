using System;
using System.Collections;
using System.Collections.Generic;
using Platformer.Mechanics;
using UnityEngine;

public class SpawnPointManager : MonoBehaviour
{
    private GameManager _gameManager;

    private void Start()
    {
        _gameManager = GameManager.Instance;
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        _gameManager.SwitchSpawnPoint(gameObject);
    }
}
