using UnityEngine;

namespace Code.AbilitySystem.Core
{
    public interface IAbilityControl
    {
        float GetAxis(int axisId);

        Vector2 GetAxis2D(int axisId);

        bool GetState(int stateId);
        
        void SetAxis(int axisId, float value);
        
        void SetAxis2D(int axisId, Vector2 value);

        void SetState(int stateId, bool value);
    }
}