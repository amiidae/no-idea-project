using System;
using System.Collections.Generic;
using Bnny.Scripts.SaveSystem;
using Bnny.Scripts.Services;
using Bnny.Scripts.Services.Data;
using Bnny.Scripts.Services.Physics;
using Bnny.Scripts.Services.Time;
using UnityEngine;

namespace Bnny.Scripts
{
    public class HeroController : MonoBehaviour
    {
        public event Action Landed;

        public bool IsGrounded { get; private set; } = true;
        public bool IsFacedAgainstWall { get; private set; } = false;

        public int NumberOfJumpsLeft { get; private set; }

        public float VerticalVelocity
        {
            get { return Rb.linearVelocityY; }
        }

        [field: SerializeField]
        public Animator Animator { get; private set; }

        [SerializeField]
        private SpriteRenderer SpriteRenderer;

        [SerializeField]
        private Rigidbody2D Rb;

        [SerializeField]
        private Collider2D heroCollider;

        [SerializeField]
        private HeroProgressManager heroProgressManager;

        private float moveVelocity;
        private float airMoveVelocity;

        private IPhysics2DService physics2DService;
        private IDataService dataRepository;
        private ITimeService timeService;

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

        void OnEnable()
        {
            Landed += OnLanded;
            heroProgressManager.ProgressLoaded += OnProgressLoaded;
        }

        void OnDisable()
        {
            Landed -= OnLanded;
            heroProgressManager.ProgressLoaded -= OnProgressLoaded;
        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            GetServices();

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
                timeService.DeltaTime
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
                dataRepository.HeroData.AirSmoothing,
                Mathf.Infinity,
                timeService.DeltaTime
            );
            PositionSprite(axis);
        }

        public void Jump()
        {
            ExecuteJump();
            JumpNumberUpdate();
        }

        private void ExecuteJump()
        {
            // Derive launch speed from gravity so the arc peaks at exactly JumpHeight metres
            // (kit-style): v = sqrt(2 * g * h). g is the body's actual gravity magnitude.
            float gravity = Mathf.Abs(Physics2D.gravity.y * Rb.gravityScale);
            Rb.linearVelocityY = Mathf.Sqrt(2f * gravity * dataRepository.HeroData.JumpHeight);
        }

        public void LongJump()
        {
            Rb.linearVelocityY =
                Rb.linearVelocityY
                + dataRepository.HeroData.JumpAcceleration * timeService.DeltaTime;
        }

        public void WallJump()
        {
            Vector2 lookDirection = SpriteRenderer.flipX == true ? Vector2.left : Vector2.right;
            Rb.linearVelocityX = -lookDirection.x * dataRepository.HeroData.WallJumpPushForce;

            ExecuteJump();

            ForceFlip();
        }

        private void OnLanded()
        {
            JumpNumberReset();
        }

        private void OnProgressLoaded(Vector3 coordinates)
        {
            ChangePosition(coordinates);
        }

        private void ChangePosition(Vector3 newPosition)
        {
            gameObject.transform.position = newPosition;
        }

        private void GetServices()
        {
            physics2DService = ServiceLocator.GetService<IPhysics2DService>();
            dataRepository = ServiceLocator.GetService<IDataService>();
            timeService = ServiceLocator.GetService<ITimeService>();
        }

        private void JumpNumberReset()
        {
            NumberOfJumpsLeft = dataRepository.HeroData.MaxNumberOfJumps;
        }

        private void JumpNumberUpdate()
        {
            --NumberOfJumpsLeft;
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
                1 << 7
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

        private void ForceFlip()
        {
            SpriteRenderer.flipX = !SpriteRenderer.flipX;
        }
    }
}
