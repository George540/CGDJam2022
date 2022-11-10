using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [Header("Gate Properties")]
    public int _keysToUnlock;
    public Animator _doorAnimator;
    public bool isDoorOpen;
    public bool isTerminal;
    public bool isStartDoor;
    
    [Header("Room Properties")]
    public Transform cameraTransform;
    public Transform PlayerSpawnTransform;
    public Door _smallDoor;

    private void Start()
    {
        if (_keysToUnlock == 0)
        {
            OpenDoor();
        }
    }

    public void OpenDoor()
    {
        if (isTerminal || isDoorOpen) return;
        
        _doorAnimator.Play("Door Open");
        GameManager.Instance._audioManager.OpenDoor();
        isDoorOpen = true;
    }

    public void OpenSmallDoor()
    {
        if (_smallDoor._keysToUnlock == 0)
        {
            _smallDoor.OpenDoor();
            GameManager.Instance._statusBar.UpdateCountdown(_keysToUnlock);
        }
    }

    public void MoveCamera()
    {
        if (Camera.main is not null){ Camera.main.transform.position = new Vector3(cameraTransform.position.x, cameraTransform.position.y, Camera.main.transform.position.z);}
    }

    public void LeaveRoom()
    {
        if (!isDoorOpen) return;
        
        isDoorOpen = false;
        _doorAnimator.Play("Door Close");
        GameManager.Instance._audioManager.CloseDoor();
    }
}
