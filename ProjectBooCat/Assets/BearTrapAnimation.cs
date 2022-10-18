using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer.Mechanics
{

    public class BearTrapAnimation : MonoBehaviour
    {
        // Start is called before the first frame update
        [SerializeField] private Animator _animator;

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.GetComponent<PlayerController>()) _animator.Play("TriggerTrap");
        }
    }

}
