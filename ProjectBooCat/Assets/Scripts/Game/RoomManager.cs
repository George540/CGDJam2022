using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public int _keysToUnlock;
    public Animator _doorAnimator;
    public bool isDoorOpen;
    public Transform cameraTransform;
    public Transform PlayerSpawnTransform;
    public bool isTerminal;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenDoor()
    {
        if (isTerminal) return;
        
        _doorAnimator.Play("Door Open");
        FindObjectOfType<AudioManager>().OpenDoor();
        isDoorOpen = true;
    }

    public void MoveCamera()
    {
        if (Camera.main is not null) Camera.main.transform.position = cameraTransform.position;
    }
    
    
}
