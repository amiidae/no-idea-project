using System;
using UnityEngine;
using UnityEngine.PlayerLoop;

// LocomotionState
// Idle: Idle, Walk, Run, Fall  IdleStateMachine 
// Action: Hit, SpecialAttack, Jump ActionStateMachine
// Reaction: HitStun

public class HeroController : MonoBehaviour
{
    
    [field: SerializeField] public Animator Animator { get; private set; }
    public Rigidbody2D Rigidbody => rb;
    
    public Collider2D Collider => GetComponent<Collider2D>();
    
    [SerializeField]
    private SpriteRenderer spriteRenderer;

    //[SerializeField]
    //private Animator animator;

    [SerializeField]
    private Rigidbody2D rb;

    private IInputService inputService;
    private IPhysics2DService physics2DService;
    private IDataRepository dataRepository;

    private bool isGrounded = true;

    private float _moveVelocity;
    private float _airMoveVelocity;

    public bool IsGrounded => isGrounded;
    public float VerticalVelocity => rb.linearVelocityY;

    public float FacingX => spriteRenderer.flipX ? -1f : 1f;

    public event Action Landed;
    public event Action WallJumped;

    void OnDrawGizmosSelected()
    {
        Gizmos.color = isGrounded ? Color.green : Color.red;
        Gizmos.DrawSphere(gameObject.transform.position, 0.25f);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InitializeServices();
    }

    void FixedUpdate()
    {
        GroundCheck();
    }

    public void Move(float axis, float speed, float smoothing)
    {
        float target = axis * speed;
        rb.linearVelocityX = Mathf.SmoothDamp(
            rb.linearVelocityX, target, ref _moveVelocity,
            smoothing, Mathf.Infinity, Time.deltaTime);
        FaceDirection(axis);
    }

    public void AirMove(float axis, float speed)
    {
        float target = axis * speed;
        rb.linearVelocityX = Mathf.SmoothDamp(
            rb.linearVelocityX, target, ref _airMoveVelocity,
            dataRepository.HeroData.AirSmoothing, Mathf.Infinity, Time.deltaTime);
        FaceDirection(axis);
    }

    public void Jump()
    {
        // Derive launch speed from gravity so the arc peaks at exactly JumpHeight metres
        // (kit-style): v = sqrt(2 * g * h). g is the body's actual gravity magnitude.
        float gravity = Mathf.Abs(Physics2D.gravity.y * rb.gravityScale);
        rb.linearVelocityY = Mathf.Sqrt(2f * gravity * dataRepository.HeroData.JumpHeight);
    }
    
    public void WallJump(Vector2 wallNormal)
    {
        Jump();

        rb.linearVelocityX =
            wallNormal.x * rb.linearVelocityY * dataRepository.HeroData.WallJumpHorizontalMultiplier;

        _airMoveVelocity = 0f;
        WallJumped?.Invoke();
    }

    public void ApplyJumpAcceleration()
    {
        rb.linearVelocityY += dataRepository.HeroData.HoldJumpAcceleration * Time.deltaTime;
    }

    public void FaceDirection(float horizontalInput)
    {
        if (horizontalInput > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (horizontalInput < 0)
        {
            spriteRenderer.flipX = true;
        }
    }

    private void InitializeServices()
    {
        inputService = ServiceLocator.GetService<IInputService>();
        physics2DService = ServiceLocator.GetService<IPhysics2DService>();
        dataRepository = ServiceLocator.GetService<IDataRepository>();
    }

    private void GroundCheck()
    {
        Collider2D playersCollision = physics2DService.OverlapCircle(
            gameObject.transform.position,
            0.25f,
            LayerMasks.SurfaceMask
        );

        bool grounded = playersCollision != null;

        if (grounded && !isGrounded)
        {
            _moveVelocity = 0f;
            Landed?.Invoke();
        }
        else if (!grounded && isGrounded)
        {
            _airMoveVelocity = 0f;
        }

        isGrounded = grounded;
    }


}
