using System;
using UnityEngine;

[Serializable]
public class HeroData
{
    public float MovementSpeed;
    public float RunSpeed
    {
        get
        {
            float runSpeed = MovementSpeed * runSpeedCoefficient;
            return runSpeed;
        }
    }



    [SerializeField]
    private float runSpeedCoefficient;


    public float JumpHeight;
    public int MaxAirJumps;
    public float HoldJumpAcceleration;

    public float WalkSmoothing;
    public float RunSmoothing;
    public float AirSmoothing;

    public float DetectionDistance;

    public float WallSlideAngle;

    public float WallSlideFriction;

    public float WallJumpHorizontalMultiplier;
}
