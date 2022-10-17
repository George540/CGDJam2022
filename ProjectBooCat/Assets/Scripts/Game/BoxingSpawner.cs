using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxingSpawner : MonoBehaviour
{
    public Transform _dropPlace;
    public GameObject _dropItem;
    public GameObject _bomb;
    public bool _hasItemDropped;
    public bool isWeightTrap;

    public void DropItem()
    {
        if (_hasItemDropped) return;
        
        if (!isWeightTrap)
        {
            var drop = Instantiate(_dropItem, _dropPlace.position, Quaternion.identity);
            drop.GetComponent<Rigidbody2D>().simulated = true;
        }
        else
        {
            _bomb.transform.parent = null;
            _bomb.GetComponent<Rigidbody2D>().simulated = true;
            _hasItemDropped = true;
        }
    }

}
