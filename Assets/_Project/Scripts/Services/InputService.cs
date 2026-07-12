using UnityEngine;

public class InputService : IInputService
{
    public Vector2 MoveAxis
    {
        get
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            Vector2 inputVector = new Vector2(horizontalInput, verticalInput);

            return inputVector;
        }
    }

    public bool IsJumping
    {
        get
        {
            bool isJumpKeyDown = Input.GetKeyDown(KeyCode.Space);
            return isJumpKeyDown;
        }
    }

    public bool IsRunning
    {
        get
        {
            bool isRunKeyDown = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
            return isRunKeyDown;
        }
    }
}
