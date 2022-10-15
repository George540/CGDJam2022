using System.Collections;
using System.Collections.Generic;
using Platformer.Mechanics;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

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
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            SwitchPlayerState();
        }
    }

    private void SwitchPlayerState()
    {
        if (_alivePlayer.activeSelf)
        {
            _playerController.enabled = false;
            _alivePlayer.SetActive(false);
            
            _ghostPlayer.SetActive(true);
            _ghostController.enabled = true;
            SetItemsVisibility(false);
        }
        else
        {
            _ghostPlayer.SetActive(false);
            _ghostController.enabled = false;
            
            _playerController.enabled = true;
            _alivePlayer.SetActive(true);
            SetItemsVisibility(true);
        }
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
    
}
