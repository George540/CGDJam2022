using System.Collections;
using System.Collections.Generic;
using Platformer.Mechanics;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public bool IsGhost;

    private GameObject spawnPoint;
    [SerializeField] private GameObject _alivePlayer;
    [SerializeField] private PlayerController _playerController;

    [SerializeField] private GameObject _ghostPlayer;
    [SerializeField] private GhostController _ghostController;

    [SerializeField] private List<GameObject> _aliveItems;
    [SerializeField] private List<GameObject> _ghostItems;
    

    private void Awake() 
    { 
        // If there is an instance, and it's not me, delete myself.
    
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        }

        IsGhost = false;
    }

    // Update is called once per frame
    void Update()
    {
        // For Debug purposes
        /*if (Input.GetKeyDown(KeyCode.E))
        {
            SwitchPlayerState();
        }*/

        if (Vector3.Distance(_ghostPlayer.transform.position, _alivePlayer.transform.position) <= 2f
            && Input.GetKeyDown(KeyCode.E))
        {
            BecomeAlive();
        }
    }

    private void SwitchPlayerState()
    {
        if (_alivePlayer.activeSelf)
        {
            _playerController.enabled = false;
            TeleportPlayer();
            
            _ghostPlayer.SetActive(true);
            _ghostController.enabled = true;
            SetItemsVisibility(false);
            SetCharacterStateCheck(false);
        }
        else
        {
            _ghostPlayer.SetActive(false);
            _ghostController.enabled = false;

            _playerController.enabled = true;
            SetItemsVisibility(true);
            SetCharacterStateCheck(true);
        }
    }

    public void BecomeAlive()
    {
        _ghostPlayer.SetActive(false);
        _ghostController.enabled = false;

        _playerController.enabled = true;
        SetItemsVisibility(true);
        SetCharacterStateCheck(true);
    }

    public void SwitchToGhostState()
    {
        SwitchPlayerState();
    }

    private void SetItemsVisibility(bool isAlive)
    {
        _aliveItems.ForEach(i => i.SetActive(isAlive));
        _ghostItems.ForEach(i => i.SetActive(!isAlive));
    }

    private void SetCharacterStateCheck(bool isGhost)
    {
        IsGhost = isGhost;
    }
    
    public void SwitchSpawnPoint(GameObject newSpawnPoint)
    {
        spawnPoint = newSpawnPoint;
    }

    public void TeleportPlayer()
    {
        _alivePlayer.transform.position = spawnPoint.transform.position;
    }

}
