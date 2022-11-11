using System.Collections;
using UnityEngine;

public class BoxingSpawner : MonoBehaviour
{
    public AudioSource _audioSource;
    public Transform _dropPlace;
    public GameObject _fakeCollider;
    public GameObject _dropItem;
    public GameObject _bombObject;
    public GameObject _bomb;
    public bool _hasItemDropped;
    public bool _isWeightTrap;

    private void Start()
    {
        if (_isWeightTrap)
        {
            SpawnBomb();
        }
    }

    public void DropItem()
    {
        if (_hasItemDropped) return;

        if (_fakeCollider)
        {
            _fakeCollider.GetComponent<Collider2D>().enabled = false;
            _fakeCollider.SetActive(false);
        }
        if (!_isWeightTrap)
        {
            var drop = Instantiate(_dropItem, _dropPlace.position, Quaternion.identity);
            drop.GetComponent<Rigidbody2D>().simulated = true;
        }
        else
        {
            _bombObject.transform.parent = null;
            _bombObject.GetComponent<Rigidbody2D>().simulated = true;
            _bombObject = null;
            _hasItemDropped = true;
            StartCoroutine(EquipNewWeight());
        }
    }

    private IEnumerator EquipNewWeight()
    {
        yield return new WaitForSeconds(10.0f);
        SpawnBomb();
        _audioSource.Play();
    }

    private void SpawnBomb()
    {
        _bombObject = Instantiate(_bomb, transform);
        _bombObject.GetComponent<Rigidbody2D>().simulated = false;
        _hasItemDropped = false;
    }

}
