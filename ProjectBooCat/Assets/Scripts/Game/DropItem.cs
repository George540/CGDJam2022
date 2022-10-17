using Platformer.Mechanics;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    [SerializeField] AudioClip _droplet;
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.collider.isTrigger)
        {
            if (other.gameObject.TryGetComponent<PlayerController>(out var player))
            {
                GameManager.Instance.SwitchToGhostState();
                GameManager.Instance._audioManager.Play(_droplet);
            }
            Destroy(gameObject);
        }
    }
}
