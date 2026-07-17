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

    public float JumpForce;
}
