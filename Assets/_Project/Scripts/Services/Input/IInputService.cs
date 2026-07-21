using UnityEngine;

public interface IInputService
{
    public Vector2 MoveAxis { get; }

    public bool IsMoveInputReceived { get; }
    public bool IsJumpInputReceived { get; }
    public bool IsRunInputReceived { get; }
}
