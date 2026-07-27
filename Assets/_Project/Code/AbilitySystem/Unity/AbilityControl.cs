using System.Collections.Generic;
using Code.AbilitySystem.Core;
using UnityEngine;

namespace Code.AbilitySystem.Unity
{
    public class AbilityControl : MonoBehaviour, IAbilityControl
    {
        private readonly Dictionary<int, float> _axes = new();
        
        private readonly Dictionary<int, Vector2> _axes2D = new();
        
        private readonly Dictionary<int, bool> _states = new();
        
        public float GetAxis(int axisId)
        {
            return _axes.GetValueOrDefault(axisId, 0f);
        }

        public Vector2 GetAxis2D(int axisId)
        {
            return _axes2D.GetValueOrDefault(axisId, Vector2.zero);
        }

        public bool GetState(int stateId)
        {
            return _states.GetValueOrDefault(stateId, false);
        }

        public void SetAxis(int axisId, float value)
        {
            _axes[axisId] = value;
        }

        public void SetAxis2D(int axisId, Vector2 value)
        {
            _axes2D[axisId] = value;
        }

        public void SetState(int stateId, bool value)
        {
            _states[stateId] = value;
        }
    }
}