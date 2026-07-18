using System;
using UnityEngine;
using UnityEngine.PlayerLoop;

// LocomotionState
// Idle: Idle, Walk, Run, Fall  IdleStateMachine 
// Action: Hit, SpecialAttack, Jump ActionStateMachine
// Reaction: HitStun

public abstract class HeroState
{
    public abstract HeroState GetState();
}

public class IdleState : HeroState
{
    
    public override HeroState GetState()
    {
        return null;
    }
}

public class WalkState : HeroState
{
    public bool CanEnter
    {
        get
        {
            return Input.GetAxisRaw("Horizontal") != 0;
        }
    }
    
    public override HeroState GetState()
    {
        return null;
    }
}


public class HeroController : MonoBehaviour
{
    [field: SerializeField] public Animator Animator { get; private set; }

   

    [SerializeField] private float _foo;
    
    [SerializeField]
    private SpriteRenderer spriteRenderer;

    //[SerializeField]
    //private Animator animator;

    [SerializeField]
    private Rigidbody2D rb;

    private IInputService inputService;
    private IPhysics2DService physics2DService;
    private IHeroDataRepository heroDataRepository;

    private float horizontalInput;
    private float currentSpeed;

    private bool isJumpInputReceived;
    private bool isRunInputReceived;

    private bool isGrounded;

    private int hashedAnimatorParameter_LinearVelocityY = Animator.StringToHash("LinearVelocityY");

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

    // Update is called once per frame
    void Update()
    {
        UpdateInputs();
        UpdateCurrentSpeed();
        UpdateMovementAnimation();

        PositionSprite(horizontalInput);
    }

    void FixedUpdate()
    {
        GroundCheck();
        PhysicalMovement();
    }

    private void InitializeServices()
    {
        inputService = ServiceLocator.GetService<IInputService>();
        physics2DService = ServiceLocator.GetService<IPhysics2DService>();
        heroDataRepository = ServiceLocator.GetService<IHeroDataRepository>();
    }

    private void UpdateInputs()
    {
        horizontalInput = inputService.MoveAxis.x;

        isJumpInputReceived = isJumpInputReceived ? true : inputService.IsJumpInputReceived;
        isRunInputReceived = isRunInputReceived ? true : inputService.IsRunInputReceived;
    }

    private void UpdateCurrentSpeed()
    {
        if (horizontalInput != 0)
        {
            currentSpeed = heroDataRepository.Data.MovementSpeed;

            if (isRunInputReceived == true)
            {
                currentSpeed = heroDataRepository.Data.RunSpeed;
            }
        }
        else
        {
            currentSpeed = 0f;
        }
    }

    private void UpdateMovementAnimation()
    {
        // magic of math states that this should
        // normalize currentSpeed to contain itself in the range from 0 to 1
        float value;

        if (currentSpeed == 0)
        {
            value = 0f;
        }
        else if (currentSpeed == heroDataRepository.Data.MovementSpeed)
        {
            value = 0.5f;
        }
        else
        {
            value = 1f;
        }

        Animator.SetFloat(hashedAnimatorParameter_LinearVelocityY, value);
    }

    private void PositionSprite(float horizontalInput)
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

    private void GroundCheck()
    {
        Collider2D playersCollision = physics2DService.OverlapCircle(
            gameObject.transform.position,
            0.25f,
            1 << 7
        );
        if (playersCollision != null)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }

    private void PhysicalMovement()
    {
        if (isRunInputReceived)
        {
            ConsumeInput(ref isRunInputReceived);
        }

        SetLinearVelocityY(horizontalInput, currentSpeed);

        if (isJumpInputReceived == true)
        {
            ConsumeInput(ref isJumpInputReceived);
            Jump();
        }

        void Jump()
        {
            rb.linearVelocityY = heroDataRepository.Data.JumpForce;
        }
    }

    private void SetLinearVelocityY(float horizontalInput, float speed)
    {
        rb.linearVelocityX = horizontalInput * speed;
    }

    private void ConsumeInput<T>(ref T input)
    {
        input = default;
    }
}
