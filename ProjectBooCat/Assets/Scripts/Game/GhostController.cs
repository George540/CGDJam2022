using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private float _acceleration;
    [SerializeField] private float _maxSpeed;

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W))
        {
            _rigidbody2D.AddForce(_acceleration * Vector2.up * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            _rigidbody2D.AddForce(_acceleration * Vector2.left * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            _rigidbody2D.AddForce(_acceleration * Vector2.down * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            _rigidbody2D.AddForce(_acceleration * Vector2.right * Time.deltaTime);
        }

        if (_rigidbody2D.velocity.magnitude > _maxSpeed)
        {
            _rigidbody2D.velocity = _rigidbody2D.velocity.normalized * _maxSpeed;
        }
    }
}
