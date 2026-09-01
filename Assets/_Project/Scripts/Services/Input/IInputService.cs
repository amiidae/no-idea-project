using System;
using UnityEngine;

public interface IInputService
{
    public event Action Save;
    public event Action ToggleDebug;
    public Vector2 MoveAxis { get; }

    public bool IsMoveInputReceived { get; }
    public bool IsRunInputReceived { get; }
    public bool IsJumpInputReceived { get; }
    public bool IsJumpInputContinuous { get; }
}
