using UnityEngine;

public class InputService : IInputService
{
    public Vector2 MoveAxis
    {
        get
        {
            // logical inconsistency
            // need to lose the y input
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            Vector2 inputVector = new Vector2(horizontalInput, verticalInput);

            return inputVector;
        }
    }

    public bool IsMoveInputReceived
    {
        get { return MoveAxis.x > 0; }
    }

    public bool IsJumpInputReceived
    {
        get
        {
            bool isJumpKeyDown = Input.GetKeyDown(KeyCode.Space);
            return isJumpKeyDown;
        }
    }

    public bool IsJumpInputContinuous
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
