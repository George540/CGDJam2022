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
        [SerializeField] private Animator _animator;
        public AudioSource _audioSource;
        public AudioClip _bearTrap;
        public AudioClip _bearTrapOpen;
        public float _openTimer;
        public bool _isClosed;
        

        private void Start()
        {
            _gameManager = GameManager.Instance;
        }

        void OnTriggerEnter2D(Collider2D collider)
        {
            var p = collider.gameObject.GetComponent<PlayerController>();
            if (p != null && p._isAlive && p.controlEnabled && !_isClosed)
            {
                _animator.Play("TriggerTrap");
                _audioSource.PlayOneShot(_bearTrap);
                _gameManager.SwitchToGhostState();
                _isClosed = true;
                StartCoroutine(OpenBearTrap());
            }
        }

        private IEnumerator OpenBearTrap()
        {
            yield return new WaitForSeconds(_openTimer);
            _audioSource.PlayOneShot(_bearTrapOpen);
            _animator.Play("TriggerOpen");
        }

        public void ActivateBearTrap()
        {
            _isClosed = false;
        }
    }
}