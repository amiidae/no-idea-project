using UnityEngine;

public class HeroController : MonoBehaviour
{
    [SerializeField]
    private HeroConfig heroConfig;

    [SerializeField]
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private Rigidbody2D rb;

    private IInputService inputService;
    private ITimeService timeService;

    private float speed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inputService = ServiceLocator.GetService<IInputService>();
        timeService = ServiceLocator.GetService<ITimeService>();
    }

    // Update is called once per frame
    void Update() { }

    void FixedUpdate()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        float horizontalInput = inputService.MoveAxis.x;

        if (horizontalInput != 0)
        {
            PositionSprite(horizontalInput);

            speed = heroConfig.HeroData.MovementSpeed;
            SetLinearVelocityY(horizontalInput, speed);

            SetAnimationWalk();

            if (inputService.IsRunning)
            {
                speed = heroConfig.HeroData.MovementSpeed * heroConfig.HeroData.RunSpeedCoefficient;
                SetLinearVelocityY(horizontalInput, speed);

                SetAnimationRun();
            }
        }
        else
        {
            //recompile
            SetAnimationIdle();
        }

        if (inputService.IsJumping == true)
        {
            Jump();
        }
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

    private void SetLinearVelocityY(float horizontalInput, float speed)
    {
        rb.linearVelocityX = horizontalInput * speed * timeService.DeltaTime;
    }

    private void Jump()
    {
        rb.linearVelocityY = heroConfig.HeroData.JumpForce;
    }

    private void SetAnimationWalk()
    {
        animator.SetBool("isWalking", true);
        animator.SetBool("isRunning", false);
    }

    private void SetAnimationRun()
    {
        animator.SetBool("isRunning", true);
        animator.SetBool("isWalking", false);
    }

    private void SetAnimationIdle()
    {
        animator.SetBool("isRunning", false);
        animator.SetBool("isWalking", false);
    }
}
