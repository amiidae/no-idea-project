using System;
using UnityEngine;

namespace Bnny.Scripts
{
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
        public float JumpAcceleration;
        public float WallJumpPushForce;

        public int MaxNumberOfJumps;
        public int MaxJumpDuration;

        public float WalkSmoothing;
        public float RunSmoothing;
        public float AirSmoothing;

        [SerializeField]
        private float runSpeedCoefficient;
    }
}
