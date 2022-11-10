using Platformer.Mechanics;
using Unity.Mathematics;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    [SerializeField] AudioClip _droplet;
    [SerializeField] private GameObject _splashPrefab;
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log(other.gameObject.name);
        if (!other.collider.isTrigger)
        {
            if (other.gameObject.TryGetComponent<PlayerController>(out var player))
            {
                GameManager.Instance.SwitchToGhostState();
            }

            if (_splashPrefab)
            {
                SpawnSplash();
            }
            GameManager.Instance._audioManager.Play(_droplet);
            Destroy(gameObject);
        }
    }

    public void SpawnSplash()
    {
        Instantiate(_splashPrefab, transform.position, quaternion.identity);
    }
}
