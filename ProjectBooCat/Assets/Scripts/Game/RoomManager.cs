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
        _doorAnimator.Play("Door Open");
        isDoorOpen = true;
    }

    public void MoveCamera()
    {
        if (Camera.main is not null) Camera.main.transform.position = cameraTransform.position;
    }
    
    
}
