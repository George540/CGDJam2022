using System.Collections;
using UnityEngine;
using Platformer.Gameplay;
using static Platformer.Core.Simulation;
using Platformer.Model;
using Platformer.Core;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace Platformer.Mechanics
{
    /// <summary>
    /// This is the main class used to implement control of the player.
    /// It is a superset of the AnimationController class, but is inlined to allow for any kind of customisation.
    /// </summary>
    public class PlayerController : KinematicObject
    {
        public AudioClip jumpAudio;
        public AudioClip deathAudio;
        public AudioClip reviveAudio;
        public AudioClip collectAudio;
        public AudioClip laserHit;

        /// <summary>
        /// Max horizontal speed of the player.
        /// </summary>
        public float maxSpeed = 7;

        /// <summary>
        /// Initial jump velocity at the start of a jump.
        /// </summary>
        public float jumpTakeOffSpeed = 7;

        public JumpState jumpState = JumpState.Grounded;
        private bool stopJump;

        public Rigidbody2D _rigidbody2D;

        /*internal new*/
        public Collider2D collider2d;

        /*internal new*/
        public AudioSource audioSource;
        public Health health;
        public bool controlEnabled = true;

        bool jump;
        public bool _isAlive;
        public bool _isFacingRight;
        public Vector2 move;
        SpriteRenderer spriteRenderer;
        internal Animator animator;
        [SerializeField] private GameObject _bubble;
        [SerializeField] private Animator _bubbleAnimator;
        [SerializeField] private GameObject _dustFX;
        readonly PlatformerModel model = Simulation.GetModel<PlatformerModel>();

        public Bounds Bounds => collider2d.bounds;

        void Awake()
        {
            health = GetComponent<Health>();
            audioSource = GetComponent<AudioSource>();
            collider2d = GetComponent<Collider2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            animator = GetComponent<Animator>();
        }

        protected override void Update()
        {
            /*if (controlEnabled)
            {
                move.x = Input.GetAxis("Horizontal");
                if (jumpState == JumpState.Grounded && Input.GetButtonDown("Jump"))
                    jumpState = JumpState.PrepareToJump;
                else if (Input.GetButtonDown("Jump"))
                {
                    stopJump = true;
                    Schedule<PlayerStopJump>().player = this;
                }
            }
            else
            {
                move.x = 0;
            }
            */

            UpdateJumpState();
            base.Update();
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            if (!controlEnabled && !_isAlive)
            {
                move.x = 0.0f;
                return;
            }
            
            if (controlEnabled && _isAlive)
            {
                var readValue = context.ReadValue<float>();
                if (readValue > 0.0f)
                {
                    move.x = 1.0f;
                }
                else if (readValue < 0.0f)
                {
                    move.x = -1.0f;
                }
                else
                {
                    move.x = 0.0f;
                }
            }
            else
            {
                move.x = 0.0f;
            }
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            if (controlEnabled)
            {
                if (jumpState == JumpState.Grounded && context.performed)
                {
                    jumpState = JumpState.PrepareToJump;
                    stopJump = true;
                    Schedule<PlayerStopJump>().player = this;
                    Instantiate(_dustFX, transform.position, Quaternion.identity);
                    audioSource.PlayOneShot(jumpAudio);
                }
            }
        }
        
        public void OnInteract(InputAction.CallbackContext context)
        {
            if(context.performed)
                GameManager.Instance.IsInRangeOfInteractable();
        }

        void UpdateJumpState()
        {
            jump = false;
            switch (jumpState)
            {
                case JumpState.PrepareToJump:
                    jumpState = JumpState.Jumping;
                    jump = true;
                    stopJump = false;
                    break;
                case JumpState.Jumping:
                    if (!IsGrounded)
                    {
                        Schedule<PlayerJumped>().player = this;
                        jumpState = JumpState.InFlight;
                    }
                    break;
                case JumpState.InFlight:
                    if (IsGrounded)
                    {
                        Schedule<PlayerLanded>().player = this;
                        jumpState = JumpState.Landed;
                    }
                    break;
                case JumpState.Landed:
                    jumpState = JumpState.Grounded;
                    break;
            }
        }

        protected override void ComputeVelocity()
        {
            if (jump && IsGrounded)
            {
                velocity.y = jumpTakeOffSpeed * model.jumpModifier;
                audioSource.PlayOneShot(jumpAudio);
                jump = false;
            }
            else if (stopJump)
            {
                stopJump = false;
                if (velocity.y > 0)
                {
                    velocity.y = velocity.y * model.jumpDeceleration;
                }
            }

            if (move.x > 0.01f)
            {
                _isFacingRight = true;
            }
            else if (move.x < -0.01f)
            {
                _isFacingRight = false;
            }

            animator.SetBool("grounded", IsGrounded);
            animator.SetFloat("velocityX", velocity.x / maxSpeed);

            targetVelocity = move * maxSpeed;
        }

        public void SetControlled(bool ctrl)
        {
            controlEnabled = ctrl;
            _isAlive = ctrl;
            move.x = 0.0f;
            if (!ctrl)
            {
                if (_isFacingRight)
                {
                    animator.Play("Impact Right");
                }
                else
                {
                    animator.Play("Impact Left");
                }
            }
            else
            {
                animator.Play("Idle Right");
            }
            //_rigidbody2D.simulated = ctrl;
            //collider2d.enabled = ctrl;
        }
        
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.CompareTag("Collectible"))
            {
                GameManager.Instance.AddKey();
                GameManager.Instance._currentRoom._keysToUnlock--;
                int keys = GameManager.Instance._currentRoom._keysToUnlock;
                if (GameManager.Instance._currentRoom._smallDoor &&
                    GameManager.Instance._currentRoom._smallDoor._keysToUnlock > 0)
                {
                    GameManager.Instance._currentRoom._smallDoor._keysToUnlock--;
                    keys = GameManager.Instance._currentRoom._smallDoor._keysToUnlock;
                }
                GameManager.Instance._statusBar.UpdateCountdown(keys);

                if (col.gameObject.layer == 8 && GameManager.Instance._aliveItems.Count > 0) // 8 = AliveItems
                {
                    GameManager.Instance._aliveItems.Remove(col.gameObject);
                }
                else if (col.gameObject.layer == 9 && GameManager.Instance._ghostItems.Count > 0) // 9 = GhostItems
                {
                    GameManager.Instance._ghostItems.Remove(col.gameObject);
                }
                Instantiate(GameManager.Instance._sparklePrefab, col.transform.position, Quaternion.identity);
                Destroy(col.gameObject);
                Debug.Log("Collected Key");
                PlayCollectAnimation();

                if (GameManager.Instance._currentRoom._keysToUnlock == 0)
                {
                    GameManager.Instance._currentRoom.OpenDoor();
                }
                else
                {
                    audioSource.PlayOneShot(collectAudio);
                }

                if (GameManager.Instance._currentRoom._smallDoor)
                {
                    GameManager.Instance._currentRoom.OpenSmallDoor();
                }
            }
            
            if (col.gameObject.TryGetComponent<BoxingSpawner>(out var trap))
            {
                trap.DropItem();
            }
        }

        public void PlayCollectAnimation()
        {
            animator.Play(_isFacingRight ? "Collect Right" : "Collect Left");
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent<RoomManager>(out var door))
            {
                if (door.isTerminal)
                {
                    // Load credits scene
                    Debug.Log("GAME OVER. YOU WIN!");
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                }
                else if (door.isDoorOpen)
                {
                    GameManager.Instance.MoveToOtherRoom();
                    transform.position = GameManager.Instance._currentRoom.PlayerSpawnTransform.position;
                }
            }
        }

        public void AnimateReviveBubble()
        {
            if (!_bubble.activeSelf)
            {
                _bubble.SetActive(true);
                _bubbleAnimator.enabled = true;
                _bubbleAnimator.Play("Revive Bubble");
            }
        }

        public void DeactivateReviveBubble()
        {
            if (_bubble.activeSelf)
            {
                _bubbleAnimator.enabled = false;
                _bubble.SetActive(false);
            }
        }

        public void SpawnSparkleVFX()
        {
            Instantiate(GameManager.Instance._sparklePrefab, transform.position, Quaternion.identity);
        }

        public enum JumpState
        {
            Grounded,
            PrepareToJump,
            Jumping,
            InFlight,
            Landed
        }
    }
}