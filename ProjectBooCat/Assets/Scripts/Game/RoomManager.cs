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

    public void OpenDoor()
    {
        if (isTerminal) return;
        
        _doorAnimator.Play("Door Open");
        GameManager.Instance._audioManager.OpenDoor();
        isDoorOpen = true;
    }

    public void MoveCamera()
    {
        if (Camera.main is not null) Camera.main.transform.position = cameraTransform.position;
    }

    public void LeaveRoom()
    {
        isDoorOpen = false;
        _doorAnimator.Play("Door Close");
        GameManager.Instance._audioManager.OpenDoor();
    }
}
