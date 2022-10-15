using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private float _acceleration;
    [SerializeField] private float _maxSpeed;
    private Vector2 _movementDirection;

    private void Update()
    {
        UpdateDirection();
    }

    private void FixedUpdate()
    {
        _movementDirection.x = Input.GetAxis("Horizontal");
        _movementDirection.y = Input.GetAxis("Vertical");
        _rigidbody2D.AddForce(_movementDirection * _acceleration * Time.deltaTime);

        if (_rigidbody2D.velocity.magnitude > _maxSpeed)
        {
            _rigidbody2D.velocity = _rigidbody2D.velocity.normalized * _maxSpeed;
        }
    }

    void UpdateDirection()
    {
        if (_movementDirection.x > 0.001f)
        {
            _spriteRenderer.flipX = false;
        }
        else if (_movementDirection.x < -0.001f)
        {
            _spriteRenderer.flipX = true;
        }
    }
}
