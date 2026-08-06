using UnityEngine;
using UnityEngine.InputSystem;

public class InputSystemService : IInputService
{
    public Vector2 MoveAxis => throw new System.NotImplementedException();

    public bool IsMoveInputReceived => throw new System.NotImplementedException();
    public bool IsRunInputReceived => throw new System.NotImplementedException();

    public bool IsJumpInputReceived => throw new System.NotImplementedException();

    public bool IsJumpInputContinuous => throw new System.NotImplementedException();
}
