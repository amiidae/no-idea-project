using System.Collections.Generic;
using Code.AbilitySystem.Core;
using UnityEngine;


namespace Code.AbilitySystem.Unity
{
    public class AbilityBlackboard : MonoBehaviour, IAbilityBlackboard
    {
#if  UNITY_EDITOR
        public IReadOnlyDictionary<int, float> DebugAxes => _axes;
        public IReadOnlyDictionary<int, Vector2> DebugAxes2D => _axes2D;
        public IReadOnlyDictionary<int, bool> DebugStates => _states;
#endif
        
        private readonly Dictionary<int, float> _axes = new(); // ad
        
        private readonly Dictionary<int, Vector2> _axes2D = new(); // wasd
        
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