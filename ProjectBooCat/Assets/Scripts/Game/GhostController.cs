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
        else
        {
            _movementDirection.x = 0;
        }
    }

    void UpdateDirection()
    {
        _animator.SetBool(IsRight, _isFacingRight);
        
        if (_movementDirection.x > 0.001f)
        {
            _animator.SetBool(IsRight, true);
            _isFacingRight = true;
        }
        else if (_movementDirection.x < -0.001f)
        {
            _animator.SetBool(IsRight, false);
            _isFacingRight = false;
        }
    }

    public void ActivateMovement()
    {
        _isControllable = true;
        _isFacingRight = true;
        _animator.SetBool(IsControllable, _isControllable);
        _animator.SetBool(IsRight, true);
        //GameManager.Instance.SetItemsVisibility(true);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Collectible"))
        {
            GameManager.Instance.AddKey();
            GameManager.Instance._currentRoom._keysToUnlock--;
            if (col.gameObject.layer == 8 && GameManager.Instance._aliveItems.Count > 0) // 8 = AliveItems
            {
                GameManager.Instance._aliveItems.Remove(col.gameObject);
            }
            else if (col.gameObject.layer == 9 && GameManager.Instance._ghostItems.Count > 0) // 9 = GhostItems
            {
                GameManager.Instance._ghostItems.Remove(col.gameObject);
            }
            Destroy(col.gameObject);
            Debug.Log("Collected Key");
            _animator.Play(_isFacingRight ? "Collect Right" : "Collect Left");
            
            if (GameManager.Instance._currentRoom._keysToUnlock == 0)
            {
                GameManager.Instance._currentRoom.OpenDoor();
            }
        }
    }

    public void Desummon()
    {
        _isControllable = false;
        _animator.Play("Desummon");
    }

    public void DisableGhost()
    {
        GameManager.Instance.AttachGhostToPlayer();
    }
}
