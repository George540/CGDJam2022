using UnityEngine;

public class FXProperties : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private EffectType _effectType;
    [SerializeField] private Animator _animator;
    [SerializeField] private string _animationString;
    [SerializeField] private float _timer;

    private void Start()
    {
        if (_effectType == EffectType.Dust)
        {
            if (!GameManager.Instance.IsGhost && GameManager.Instance._alivePlayer.activeSelf &&
                !GameManager.Instance._playerController._isFacingRight)
            {
                _spriteRenderer.flipX = true;
            }
        }
        _animator.Play(_animationString);
        Destroy(gameObject, _timer);
    }
}

public enum EffectType
{
    Sparkle,
    Dust,
    Splash,
}
