using System;
using UnityEngine;

public class LegacyInputManagerService : IInputService
{
    public event Action Save;
    public event Action ToggleDebug;

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
        get { return Math.Abs(MoveAxis.x) > 0; }
    }

    public bool IsRunInputReceived
    {
        get
        {
            bool isRunKeyHeld = Input.GetKey(KeyCode.LeftShift);
            return isRunKeyHeld;
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

    public bool IsJumpInputContinuous
    {
        get
        {
            bool isJumpKeyHeld = Input.GetKey(KeyCode.Space);
            return isJumpKeyHeld;
        }
    }
}
