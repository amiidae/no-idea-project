using System.Collections.Generic;
using UnityEngine;

namespace Bnny.Scripts.AbilitySystem.Core
{
    public interface IAbilityUserBlackboard
    {
        public float GetAxis(int inputTypeId);
        public Vector2 GetAxis2D(int inputTypeId);
        public bool GetState(int inputTypeId);

        public void SetAxis(int inputTypeId, float value);

        public void SetAxis2D(int inputTypeId, Vector2 value);

        public void SetState(int inputTypeId, bool value);
    }
}
