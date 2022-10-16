using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LaserTrap : MonoBehaviour
{
    [SerializeField] private LineRenderer Laser;
    [SerializeField] private EdgeCollider2D Hitbox;
    private List<Vector2> Points;
    private GameManager _gameManager;
    public bool hitPlayer;

    public LayerMask _layerMask;
    private enum HitState
    {
        Hit, NotHit
    }
    
    private HitState CurrentState;

    private void Awake()
    {
        Points = new List<Vector2>();
        Points.Add(Laser.GetPosition(0));
        CurrentState = HitState.NotHit;
        _gameManager = GameManager.Instance;
        hitPlayer = false;
        //LaserSprite.gameObject.GetComponent<Laser>().SetTrap(this);
    }

    private void Update()
    {
        if (hitPlayer)
            this.gameObject.SetActive(false);
    }

    private void FixedUpdate()
    {
        var transform1 = transform;
        var position = transform1.position;
        Debug.DrawRay(position, -transform1.up * 10f, Color.red);
        RaycastHit2D hit = Physics2D.Raycast(position,  -transform.up, 10f);
        Laser.SetPosition(0, transform.position);
        Laser.SetPosition(1, hit.transform.position);
    }
}
