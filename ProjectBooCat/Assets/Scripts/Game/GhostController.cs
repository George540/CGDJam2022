using System;
using System.Collections;
using System.Collections.Generic;
using Platformer.Mechanics;
using UnityEngine;
using UnityEngine.InputSystem;

public class GhostController : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _collectAudio;
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
            //_movementDirection.x = Input.GetAxis("Horizontal");
            //_movementDirection.y = Input.GetAxis("Vertical");
            _rigidbody2D.AddForce(_movementDirection * _acceleration * Time.deltaTime);

            if (_rigidbody2D.velocity.magnitude > _maxSpeed)
            {
                _rigidbody2D.velocity = _rigidbody2D.velocity.normalized * _maxSpeed;
            }
        }
        else
        {
            _movementDirection = new Vector2(0.0f, 0.0f);
            _rigidbody2D.velocity = _movementDirection;
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _movementDirection = context.ReadValue<Vector2>();
    }
    
    public void OnInteract(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            GameManager.Instance.IsInRangeOfInteractable();
            if (GameManager.Instance.IsGhost && GameManager.Instance.IsInReviveDistance())
            {
                GameManager.Instance.SwitchPlayerState();
            }
        }
    }

    void UpdateDirection()
    {
        if (!_isControllable) return;
        
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
            int keys = GameManager.Instance._currentRoom._keysToUnlock;
            if (GameManager.Instance._currentRoom._smallDoor &&
                GameManager.Instance._currentRoom._smallDoor._keysToUnlock > 0)
            {
                GameManager.Instance._currentRoom._smallDoor._keysToUnlock--;
                keys = GameManager.Instance._currentRoom._smallDoor._keysToUnlock;
            }
            GameManager.Instance._statusBar.UpdateCountdown(keys);

            if (col.gameObject.layer == 8 && GameManager.Instance._aliveItems.Count > 0) // 8 = AliveItems
            {
                GameManager.Instance._aliveItems.Remove(col.gameObject);
            }
            else if (col.gameObject.layer == 9 && GameManager.Instance._ghostItems.Count > 0) // 9 = GhostItems
            {
                GameManager.Instance._ghostItems.Remove(col.gameObject);
            }
            Instantiate(GameManager.Instance._sparklePrefab, col.transform.position, Quaternion.identity);
            Destroy(col.gameObject);
            Debug.Log("Collected Key");
            _animator.Play(_isFacingRight ? "Collect Right" : "Collect Left");
            if (keys != 0) _audioSource.PlayOneShot(_collectAudio);

            if (GameManager.Instance._currentRoom._keysToUnlock == 0)
            {
                GameManager.Instance._currentRoom.OpenDoor();
            }

            if (GameManager.Instance._currentRoom._smallDoor)
            {
                GameManager.Instance._currentRoom.OpenSmallDoor();
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
        SwitchAnimatorState(false);
        GameManager.Instance.AttachGhostToPlayer();
    }

    public void SwitchAnimatorState(bool isActive)
    {
        _animator.enabled = isActive;
    }
}
