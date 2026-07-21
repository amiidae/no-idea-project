using System;
using UnityEngine;

public class HeroController : MonoBehaviour
{
    [field: SerializeField]
    public HeroContext HeroContext { get; private set; }

    private IInputService inputService;
    private IPhysics2DService physics2DService;
    private IDataService DataRepository;

    void OnDrawGizmosSelected()
    {
        Gizmos.color = HeroContext.IsGrounded ? Color.green : Color.red;
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
        // UpdateMovementAnimation();

        PositionSprite(HeroContext.HorizontalInput);
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
        DataRepository = ServiceLocator.GetService<IDataService>();
    }

    private void UpdateInputs()
    {
        HeroContext.HorizontalInput = inputService.MoveAxis.x;

        HeroContext.IsJumpInputReceived = HeroContext.IsJumpInputReceived
            ? true
            : inputService.IsJumpInputReceived;
        HeroContext.IsRunInputReceived = HeroContext.IsRunInputReceived
            ? true
            : inputService.IsRunInputReceived;
    }

    private void UpdateCurrentSpeed()
    {
        if (HeroContext.HorizontalInput != 0)
        {
            HeroContext.CurrentSpeed = DataRepository.HeroData.MovementSpeed;

            if (HeroContext.IsRunInputReceived == true)
            {
                HeroContext.CurrentSpeed = DataRepository.HeroData.RunSpeed;
            }
        }
        else
        {
            HeroContext.CurrentSpeed = 0f;
        }
    }

    private void UpdateMovementAnimation()
    {
        // magic of math states that this should
        // normalize HeroContext.CurrentSpeed to contain itself in the range from 0 to 1
        float value;

        if (HeroContext.CurrentSpeed == 0)
        {
            value = 0f;
        }
        else if (HeroContext.CurrentSpeed == DataRepository.HeroData.MovementSpeed)
        {
            value = 0.5f;
        }
        else
        {
            value = 1f;
        }

        HeroContext.Animator.SetFloat(HeroContext.HashedAnimatorParameter_LinearVelocityY, value);
    }

    private void PositionSprite(float horizontalInput)
    {
        if (HeroContext.HorizontalInput > 0)
        {
            HeroContext.SpriteRenderer.flipX = false;
        }
        else if (HeroContext.HorizontalInput < 0)
        {
            HeroContext.SpriteRenderer.flipX = true;
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
            HeroContext.IsGrounded = true;
        }
        else
        {
            HeroContext.IsGrounded = false;
        }
    }

    private void PhysicalMovement()
    {
        if (HeroContext.IsRunInputReceived)
        {
            ConsumeInput(ref HeroContext.IsRunInputReceived);
        }

        SetLinearVelocityY(HeroContext.HorizontalInput, HeroContext.CurrentSpeed);

        if (HeroContext.IsJumpInputReceived == true)
        {
            ConsumeInput(ref HeroContext.IsJumpInputReceived);
            Jump();
        }

        void Jump()
        {
            HeroContext.Rb.linearVelocityY = DataRepository.HeroData.JumpForce;
        }
    }

    private void SetLinearVelocityY(float horizontalInput, float speed)
    {
        HeroContext.Rb.linearVelocityX = HeroContext.HorizontalInput * speed;
    }

    private void ConsumeInput<T>(ref T input)
    {
        input = default;
    }
}
