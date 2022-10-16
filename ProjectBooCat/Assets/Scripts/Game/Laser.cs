using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private GameManager _gameManager;

    [SerializeField] private LaserTrap LaserTrap;
    
    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameManager.Instance;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("on trigger enter");
        if (col.CompareTag("Player"))
        {
            Debug.Log("hit player");
            LaserTrap.hitPlayer = true;
            _gameManager.SwitchToGhostState();
            Destroy(this.gameObject);
        }
    }

    public void SetTrap(LaserTrap trap)
    {
        LaserTrap = trap;
    }
}
