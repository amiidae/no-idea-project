using UnityEngine;

public interface IInputService
{
    public Vector2 MoveAxis { get; }

    public bool IsJumping { get; }
    public bool IsRunning { get; }
}
