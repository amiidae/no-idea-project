using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HeroController : MonoBehaviour
{
    public event Action Landed;

    public bool IsGrounded { get; private set; } = true;
    public bool IsFacedAgainstWall { get; private set; } = false;

    public int NumberOfJumpsLeft { get; private set; }

    [field: SerializeField]
    public Animator Animator { get; private set; }

    [SerializeField]
    private SpriteRenderer SpriteRenderer;

    [SerializeField]
    private Rigidbody2D Rb;

    [SerializeField]
    private Collider2D heroCollider;

    private float moveVelocity;
    private float airMoveVelocity;

    private IPhysics2DService physics2DService;
    private IDataService DataRepository;

    void OnDrawGizmosSelected()
    {
        Gizmos.color = IsGrounded ? Color.green : Color.red;
        Gizmos.DrawSphere(gameObject.transform.position, 0.25f);

        Collider2D heroCollider = gameObject.GetComponent<Collider2D>();

        float rayDirection = SpriteRenderer.flipX == true ? -1f : 1f;

        Gizmos.DrawLine(
            new Vector3(
                gameObject.transform.position.x,
                gameObject.transform.position.y + heroCollider.bounds.extents.y,
                gameObject.transform.position.z
            ),
            new Vector3(
                gameObject.transform.position.x + rayDirection,
                gameObject.transform.position.y + heroCollider.bounds.extents.y,
                gameObject.transform.position.z
            )
        );
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InitializeServices();

        JumpNumberReset();
    }

    void FixedUpdate()
    {
        GroundCheck();
        WallCheck();
    }

    public void Move(float axis, float speed, float smoothing)
    {
        float target = axis * speed;
        Rb.linearVelocityX = Mathf.SmoothDamp(
            Rb.linearVelocityX,
            target,
            ref moveVelocity,
            smoothing,
            Mathf.Infinity,
            Time.deltaTime
        );
        PositionSprite(axis);
    }

    public void AirMove(float axis, float speed)
    {
        float target = axis * speed;
        Rb.linearVelocityX = Mathf.SmoothDamp(
            Rb.linearVelocityX,
            target,
            ref airMoveVelocity,
            DataRepository.HeroData.AirSmoothing,
            Mathf.Infinity,
            Time.deltaTime
        );
        PositionSprite(axis);
    }

    public void Jump()
    {
        // Derive launch speed from gravity so the arc peaks at exactly JumpHeight metres
        // (kit-style): v = sqrt(2 * g * h). g is the body's actual gravity magnitude.
        float gravity = Mathf.Abs(Physics2D.gravity.y * Rb.gravityScale);
        Rb.linearVelocityY = Mathf.Sqrt(2f * gravity * DataRepository.HeroData.JumpHeight);
    }

    public void LongJump()
    {
        Rb.linearVelocityY = DataRepository.HeroData.JumpHeight;
    }

    public void JumpNumberReset()
    {
        NumberOfJumpsLeft = DataRepository.HeroData.MaxNumberOfJumps;

        // Debug.Log(NumberOfJumpsLeft);
    }

    public void JumpNumberUpdate()
    {
        --NumberOfJumpsLeft;

        // Debug.Log(NumberOfJumpsLeft);
    }

    private void InitializeServices()
    {
        physics2DService = ServiceLocator.GetService<IPhysics2DService>();
        DataRepository = ServiceLocator.GetService<IDataService>();
    }

    private void GroundCheck()
    {
        Collider2D playersGroundCollision = physics2DService.OverlapCircle(
            gameObject.transform.position,
            0.25f,
            1 << 7
        );

        // драгоценная Вы моя тарталетка демоническобожественная
        // совесть имейте
        // желательно в наличии
        // bool hugs = true;
        // bool kisses = true;
        // if (hugs && kisses)
        //     print("<3");

        bool isPlayerGroundedCurrentFixedUpdate = playersGroundCollision != null;
        bool wasPlayerGroundedPreviousFixedUpdate = IsGrounded;

        if (
            isPlayerGroundedCurrentFixedUpdate == true
            && wasPlayerGroundedPreviousFixedUpdate == false
        )
        {
            // player was in the air and now on the ground
            // player has landed

            moveVelocity = 0f;

            Landed!.Invoke();
        }
        else if (
            isPlayerGroundedCurrentFixedUpdate == false
            && wasPlayerGroundedPreviousFixedUpdate == true
        )
        {
            // player was on the ground, but now he is not
            // player is jumping or falling

            airMoveVelocity = 0f;
        }

        IsGrounded = isPlayerGroundedCurrentFixedUpdate;
    }

    private void WallCheck()
    {
        Vector2 origin = new Vector2(
            gameObject.transform.position.x,
            gameObject.transform.position.y + heroCollider.bounds.extents.y
        );

        Vector2 rayDirection = SpriteRenderer.flipX == true ? Vector2.left : Vector2.right;

        RaycastHit2D playersWallCollision = physics2DService.Raycast(
            origin,
            rayDirection,
            1f,
            1 << 8
        );

        if (playersWallCollision == true)
        {
            IsFacedAgainstWall = true;
        }
        else
        {
            IsFacedAgainstWall = false;
        }
    }

    private void PositionSprite(float horizontalInput)
    {
        if (horizontalInput > 0)
        {
            SpriteRenderer.flipX = false;
        }
        else if (horizontalInput < 0)
        {
            SpriteRenderer.flipX = true;
        }
    }
}
