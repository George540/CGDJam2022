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
    [SerializeField] private GameObject _alivePlayer;
    [SerializeField] private PlayerController _playerController;

    [SerializeField] private GameObject _ghostPlayer;
    [SerializeField] private GhostController _ghostController;

    public List<GameObject> _ghostBlocks;
    public List<GameObject> _aliveItems;
    public List<GameObject> _ghostItems;

    public int _keys;
    public List<RoomManager> _rooms;
    public RoomManager _currentRoom;
    public int _currentRoomId;

    public AudioManager _audioManager;
<<<<<<< Updated upstream
=======
    public StatusBar _statusBar;
    public GameObject _sparklePrefab;

    public bool canPressLever;
    public bool playerPressLever;
>>>>>>> Stashed changes

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
<<<<<<< Updated upstream
=======
        _statusBar = FindObjectOfType<StatusBar>();
        canPressLever = false;
        playerPressLever = false;
>>>>>>> Stashed changes
    }

    // Update is called once per frame
    void Update()
    {
        // For Debug purposes
        /*if (Input.GetKeyDown(KeyCode.E))
        {
            SwitchPlayerState();
        }*/

        if (Vector3.Distance(_ghostPlayer.transform.position, _alivePlayer.transform.position) <= 0.2f
            && Input.GetKeyDown(KeyCode.E) && IsGhost)
        {
            SwitchPlayerState();
        }

        if(!_audioManager)
        {
            _audioManager = FindObjectOfType<AudioManager>();
        }
    }

    private void SwitchPlayerState()
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
            _ghostPlayer.transform.parent = null;
            SetItemsVisibility(false);
            IsGhost = true;

            if (_playerController.GetComponent<AudioSource>() && _playerController.deathAudio)
                _playerController.GetComponent<AudioSource>().PlayOneShot(_playerController.deathAudio);
        }
        else
        {
            _ghostController.Desummon();

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
        if (_statusBar) _statusBar.AddCard();
    }

    public void MoveToOtherRoom()
    {
        _currentRoomId++;
        _currentRoom = _rooms[_currentRoomId];
        _currentRoom.MoveCamera();
    }

    public void AttachGhostToPlayer()
    {
        _ghostPlayer.transform.position = _alivePlayer.transform.position;
        _ghostPlayer.transform.parent = _alivePlayer.transform;
        _ghostController.enabled = false;
        _ghostPlayer.SetActive(false);

        _playerController.SetControlled(true);
        SetItemsVisibility(true);
        IsGhost = false;
        GameManager.Instance.SwitchGhostBlocks();
    }

    public void SwitchGhostBlocks()
    {
        if (IsGhost)
        {
            _ghostBlocks.ForEach(b => b.GetComponent<Animator>().Play("GhostBlock"));
        }
        else
        {
            _ghostBlocks.ForEach(b => b.GetComponent<Animator>().Play("GhostBlockAlive"));
        }
    }
}
