using Platformer.Mechanics;
using Unity.Mathematics;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    [SerializeField] AudioClip _droplet;
    [SerializeField] private GameObject _splashPrefab;
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.collider.isTrigger)
        {
            if (other.gameObject.TryGetComponent<PlayerController>(out var player))
            {
                GameManager.Instance.SwitchToGhostState();
                GameManager.Instance._audioManager.Play(_droplet);
            }

            if (_splashPrefab)
            {
                SpawnSplash();
            }
            Destroy(gameObject);
        }
    }

    public void SpawnSplash()
    {
        Instantiate(_splashPrefab, transform.position, quaternion.identity);
    }
}
