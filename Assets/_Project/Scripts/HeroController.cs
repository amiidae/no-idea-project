using System;
using System.Collections.Generic;
using UnityEngine;

public class HeroController : MonoBehaviour
{
    public event Action Landed;

    public SpriteRenderer SpriteRenderer;

    public Animator Animator;

    public Rigidbody2D Rb;

    public float HorizontalInput;

    public float CurrentSpeed;

    public bool IsJumpInputReceived;

    public bool IsRunInputReceived;

    public bool IsGrounded;

    public void Move(float axis, float speed, float smoothing) { }

    public void AirMove(float axis, float speed) { }

    public void Jump() { }
}
