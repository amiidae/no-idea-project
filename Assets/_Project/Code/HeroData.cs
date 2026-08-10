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


    public float WalkSmoothing;
    public float RunSmoothing;
    public float AirSmoothing;


    public float WallSlideAngle;

    public float WallSlideFriction;
}
