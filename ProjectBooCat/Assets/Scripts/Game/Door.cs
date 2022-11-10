using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Animator _doorAnimator;
    public Collider2D _collider2D;
    public int _keysToUnlock;
    public bool isDoorOpen;
    
    public void OpenDoor()
    {
        if ( isDoorOpen) return;
        
        _doorAnimator.Play("Door Open");
        GameManager.Instance._audioManager.OpenDoor();
        isDoorOpen = true;
        _collider2D.enabled = false;
    }
}
