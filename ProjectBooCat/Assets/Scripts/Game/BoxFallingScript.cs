using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BoxFallingScript : MonoBehaviour
{

    [SerializeField] private SpriteRenderer Box;
    [SerializeField] private SpriteRenderer BoxCapsule;
    [SerializeField] private float NumberOfBoxes;
    [SerializeField] private float SpawnInterval;
    private float currentTime = 0f;
    public bool hitPlayer;
    private GameManager _gameManager;

    private void Start()
    {
        _gameManager = GameManager.Instance;
        hitPlayer = false;
        Box.gameObject.GetComponent<Box>().SetTrap(this);
    }

    private void Update()
    {
        if(hitPlayer)
            this.gameObject.SetActive(false);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        Debug.Log("OnTriggerStay");
        
        currentTime += Time.deltaTime;
        
        if(currentTime >= SpawnInterval)
            SpawnBox();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        CancelInvoke("SpawnBox");
    }

    private void SpawnBox()
    {
        Instantiate(Box, BoxCapsule.transform.position, Quaternion.identity);
        currentTime = 0f;
    }

}
