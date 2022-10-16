using System;
using System.Collections;
using System.Collections.Generic;
using Platformer.Gameplay;
using UnityEngine;
using static Platformer.Core.Simulation;

namespace Platformer.Mechanics
{
    /// <summary>
    /// DeathZone components mark a collider which will schedule a
    /// PlayerEnteredDeathZone event when the player enters the trigger.
    /// </summary>
    public class DeathZone : MonoBehaviour
    {
        private GameManager _gameManager;
        [SerializeField] private Collider2D _collider2D;

        private void Start()
        {
            _gameManager = GameManager.Instance;
        }

        void OnTriggerStay2D(Collider2D collider)
        {
            var p = collider.gameObject.GetComponent<PlayerController>();
            GetComponent<AudioSource>().Play();
            if (p != null)
            {
                _gameManager.SwitchToGhostState();
                _collider2D.enabled = false;
            }
        }
    }
}