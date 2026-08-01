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

    public float JumpHeight;
    public int MaxNumberOfJumps;
    public int MaxJumpDuration;

    public float WalkSmoothing;
    public float RunSmoothing;
    public float AirSmoothing;

    [SerializeField]
    private float runSpeedCoefficient;
}
