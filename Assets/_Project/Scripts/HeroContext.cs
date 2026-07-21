using System;
using UnityEngine;

[Serializable]
public class HeroContext
{
    public SpriteRenderer SpriteRenderer;

    public Animator Animator;

    public Rigidbody2D Rb;

    public float HorizontalInput;

    public float CurrentSpeed;

    public bool IsJumpInputReceived;

    public bool IsRunInputReceived;

    public bool IsGrounded;

    public int HashedAnimatorParameter_LinearVelocityY => Animator.StringToHash("LinearVelocityY");

    public int HashedAnimationName_Idle => Animator.StringToHash("Gino-Idle");

    public int HashedAnimationName_Walk => Animator.StringToHash("Gino-Walk");
    public int HashedAnimationName_Run => Animator.StringToHash("Gino-Run");

    public int HashedAnimationName_JumpStart => Animator.StringToHash("Gino-Jump-Start");
    public int HashedAnimationName_JumpLoop => Animator.StringToHash("Gino-Jump-Loop");
    public int HashedAnimationName_JumpLand => Animator.StringToHash("Gino-Jump-Land");
}
