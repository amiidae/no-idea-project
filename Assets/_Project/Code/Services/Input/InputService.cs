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

    public bool IsJumpInputReceived
    {
        get
        {
            bool isJumpKeyDown = Input.GetKeyDown(KeyCode.Space);
            return isJumpKeyDown;
        }
    }

    public bool IsLongJumpInputReceived
    {
        get
        {
            bool isJumpKeyHeld = Input.GetKey(KeyCode.Space);
            return isJumpKeyHeld;
        }
    }

    public bool IsRunInputReceived
    {
        get
        {
            bool isRunKeyDown = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
            return isRunKeyDown;
        }
    }
}
