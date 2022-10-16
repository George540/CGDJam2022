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
    [SerializeField] private Collider2D _collider2D;
    private float currentTime = 0f;
    public bool hitPlayer;
    private GameManager _gameManager;
    private SpriteRenderer _currentBoxInstance;

    private void Start()
    {
        _gameManager = GameManager.Instance;
        hitPlayer = false;
        Box.gameObject.GetComponent<Box>().SetTrap(this);
    }

    private void Update()
    {
        if (hitPlayer)
            _collider2D.enabled = false;
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
        _currentBoxInstance = Instantiate(Box, BoxCapsule.transform.position, Quaternion.identity);
        StartCoroutine("PlayAcidAnimation");
        currentTime = 0f;
    }

    private IEnumerator PlayAcidAnimation()
    {
        yield return new WaitForSeconds(0.87f);
        _currentBoxInstance.GetComponent<Box>().Rb.gravityScale =
            _currentBoxInstance.GetComponent<Box>().RbGravityScale;
    }

}
