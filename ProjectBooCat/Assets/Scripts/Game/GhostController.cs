using System;
using System.Collections;
using System.Collections.Generic;
using Platformer.Mechanics;
using UnityEngine;

public class GhostController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private Animator _animator;
    [SerializeField] private float _acceleration;
    [SerializeField] private float _maxSpeed;
    private Vector2 _movementDirection;
    private bool _isFacingRight;
    private bool _isControllable;
    
    private static readonly int IsRight = Animator.StringToHash("IsRight");
    private static readonly int IsControllable = Animator.StringToHash("IsControllable");

    private void Update()
    {
        UpdateDirection();
    }

    private void FixedUpdate()
    {
        if (_isControllable)
        {
            _movementDirection.x = Input.GetAxis("Horizontal");
            _movementDirection.y = Input.GetAxis("Vertical");
            _rigidbody2D.AddForce(_movementDirection * _acceleration * Time.deltaTime);

            if (_rigidbody2D.velocity.magnitude > _maxSpeed)
            {
                _rigidbody2D.velocity = _rigidbody2D.velocity.normalized * _maxSpeed;
            }
        }
    }

    void UpdateDirection()
    {
        _animator.SetBool(IsRight, _isFacingRight);
        
        if (_movementDirection.x > 0.001f)
        {
            _animator.SetBool(IsRight, true);
        }
        else if (_movementDirection.x < -0.001f)
        {
            _animator.SetBool(IsRight, false);
        }
    }

    public void ActivateMovement()
    {
        _isControllable = true;
        _isFacingRight = true;
        _animator.SetBool(IsControllable, _isControllable);
        _animator.SetBool(IsRight, true);
    }
}
