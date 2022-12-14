using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Platformer.Mechanics;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public bool IsGhost;

    private GameObject spawnPoint;
    public GameObject _alivePlayer;
    public PlayerController _playerController;

    public GameObject _ghostPlayer;
    public GhostController _ghostController;

    public float _reviveDistance;
    public List<GameObject> _ghostBlocks;
    public List<GameObject> _aliveItems;
    public List<GameObject> _ghostItems;

    public int _keys;
    public List<RoomManager> _rooms;
    public RoomManager _currentRoom;
    public int _currentRoomId;
    public List<Lever> _levers;

    public AudioManager _audioManager;
    public GameObject _sparklePrefab;
    public StatusBar _statusBar;

    public bool canPressLever;
    public bool playerPressLever;

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

    private void Start()
    {
        _ghostPlayer.transform.position = _alivePlayer.transform.position;
        _ghostPlayer.transform.parent = _alivePlayer.transform;
        _currentRoom = _rooms[_currentRoomId];
        playerPressLever = false;
        canPressLever = false;
    }

    // Update is called once per frame
    void Update()
    {
        // For Debug purposes
        /*if (Input.GetKeyDown(KeyCode.E))
        {
            SwitchPlayerState();
        }*/

        if (IsInReviveDistance())
        {
            if (IsGhost)
            {
                _playerController.AnimateReviveBubble();
            }
        }
        else
        {
            if (IsGhost)
            {
                _playerController.DeactivateReviveBubble();
            }
        }

        if(!_audioManager)
        {
            _audioManager = FindObjectOfType<AudioManager>();
        }
    }

    public bool IsInReviveDistance()
    {
        return Vector3.Distance(_ghostPlayer.transform.position, _alivePlayer.transform.position) <= _reviveDistance;
    }

    public void SwitchPlayerState()
    {
        if (_audioManager)
        {
            StartCoroutine(_audioManager.Switch(!IsGhost));
        }
        

        if (_statusBar)
        {
            _statusBar.UpdateStatus(!IsGhost);
        }

        if (!IsGhost)
        {
            _playerController.SetControlled(false);
            //TeleportPlayer();
            
            _ghostPlayer.SetActive(true);
            _ghostController.enabled = true;
            _ghostController.SwitchAnimatorState(true);
            _ghostPlayer.transform.parent = null;
            SetItemsVisibility(false);
            IsGhost = true;
            Instantiate(_sparklePrefab, _ghostController.transform.position, Quaternion.identity);

            if (_playerController.GetComponent<AudioSource>() && _playerController.deathAudio)
                _playerController.GetComponent<AudioSource>().PlayOneShot(_playerController.deathAudio);
        }
        else
        {
            IsGhost = false;
            _playerController.DeactivateReviveBubble();
            _ghostController.Desummon();
            Instantiate(_sparklePrefab, _playerController.transform.position, Quaternion.identity);

            if (_playerController.GetComponent<AudioSource>() && _playerController.reviveAudio)
                _playerController.GetComponent<AudioSource>().PlayOneShot(_playerController.reviveAudio);
        }
        
        SwitchGhostBlocks();
    }

    public void SwitchToGhostState()
    {
        SwitchPlayerState();
    }

    public void SetItemsVisibility(bool isAlive)
    {
        if (_aliveItems.Count > 0)
        {
            _aliveItems.ForEach(i => i.SetActive(isAlive));
        }

        if (_ghostItems.Count > 0)
        {
            _ghostItems.ForEach(i => i.SetActive(!isAlive));
        }
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

    public void AddKey()
    {
        _keys++;
    }

    public void MoveToOtherRoom()
    {
        _currentRoom.LeaveRoom();
        _currentRoomId++;
        _currentRoom = _rooms[_currentRoomId];
        _currentRoom.MoveCamera();

        int keys = _currentRoom._keysToUnlock;
        if (_currentRoom._smallDoor &&
            _currentRoom._smallDoor._keysToUnlock > 0)
        {
            keys = _currentRoom._smallDoor._keysToUnlock;
        }
        _statusBar.UpdateCountdown(keys);

        if (_currentRoomId == 6)
        {
            foreach (var lever in _levers)
            {
                lever.ActivateLaserSound();
            }
        }
        else if (_currentRoomId == 7)
        {
            foreach (var lever in _levers)
            {
                lever.DeactivateLaserSound();
            }

            if (_currentRoom.TryGetComponent<AudioSource>(out var audioSource))
            {
                audioSource.Play();
            }
        }
    }

    public void AttachGhostToPlayer()
    {
        _ghostPlayer.transform.position = _alivePlayer.transform.position;
        _ghostPlayer.transform.parent = _alivePlayer.transform;
        _ghostController.enabled = false;
        _ghostPlayer.SetActive(false);

        _playerController.SetControlled(true);
        SetItemsVisibility(true);
        GameManager.Instance.SwitchGhostBlocks();
    }

    public void SwitchGhostBlocks()
    {
        if (_ghostPlayer.activeSelf)
        {
            foreach (var block in _ghostBlocks)
            {
                block.GetComponent<Animator>().Play("GhostBlockAlive");
                block.GetComponent<Collider2D>().enabled = true;
            }
        }
        else
        {
            foreach (var block in _ghostBlocks)
            {
                block.GetComponent<Animator>().Play("GhostBlock");
                block.GetComponent<Collider2D>().enabled = false;
            }
        }
    }

    public void IsInRangeOfInteractable()
    {
        if (canPressLever)
            playerPressLever = true;
        else
            playerPressLever = false;
    }
}
