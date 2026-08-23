using System;
using UnityEngine;

public interface IInputService
{
    event Action Save;
    
    public Vector2 MoveAxis { get; }
    public bool IsJumpInputReceived { get; }
    public bool IsLongJumpInputReceived { get; }
    public bool IsRunInputReceived { get; }
}
