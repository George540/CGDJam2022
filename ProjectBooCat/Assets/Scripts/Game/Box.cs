using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class Box : MonoBehaviour
{
    private GameManager _gameManager;
    public float RbGravityScale;

    [SerializeField] private BoxFallingScript BoxTrap;
    [SerializeField] public Rigidbody2D Rb;
    [SerializeField] AudioClip droplet;

    void Start()
    {
        _gameManager = GameManager.Instance;
        RbGravityScale = Rb.gravityScale;
        Rb.gravityScale = 0f;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("on trigger enter");
        if (col.CompareTag("Player"))
        {
            Debug.Log("hit player");
            BoxTrap.hitPlayer = true;
            _gameManager.SwitchToGhostState();
            FindObjectOfType<AudioManager>().Play(droplet);
            Destroy(this.gameObject);
        }
    }

    public void SetTrap(BoxFallingScript trap)
    {
        BoxTrap = trap;
    }
}
