using UnityEngine;

public class Sparkle : MonoBehaviour
{
    [SerializeField] private Animator _sparkleAnimator;

    private void Start()
    {
        _sparkleAnimator.Play("sparkle_anim");
        Destroy(gameObject, 0.6f);
    }
}
