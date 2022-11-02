using UnityEngine;

public class BubbleInteraction : MonoBehaviour
{
    [SerializeField] private Transform _targetTransform;
    [SerializeField] private Animator _bubbleAnimator;
    // Start is called before the first frame update

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<GhostController>(out var ghost))
        {
            transform.position = _targetTransform.position;
            _bubbleAnimator.enabled = true;
            _bubbleAnimator.Play("ReviveUI");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent<GhostController>(out var ghost) && _bubbleAnimator.enabled)
        {
            transform.position = Vector3.zero;
            _bubbleAnimator.enabled = false;
        }
    }
}
