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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inputService = ServiceLocator.GetService<IInputService>();
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

        rb.linearVelocityX = horizontalInput * heroConfig.HeroData.MovementSpeed * Time.deltaTime;

        if (inputService.IsJumping == true)
        {
            float verticalInput = inputService.MoveAxis.y;
            rb.linearVelocityY = heroConfig.HeroData.JumpForce;
        }
    }
}
