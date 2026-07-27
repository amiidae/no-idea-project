using Code.AbilitySystem.Core;
using UnityEngine;

namespace Code.AbilitySystem
{
    public static class AbilityInputControlTypeExtensions
    {
        public static void SetAxis(this IAbilityControl abilityControl, ControlTypeId controlTypeId, float value)
        {
            abilityControl.SetAxis((int)controlTypeId, value);
        }
        
        public static void SetAxis2D(this IAbilityControl abilityControl, ControlTypeId controlTypeId, Vector2 value)
        {
            abilityControl.SetAxis2D((int)controlTypeId, value);
        }
        
        public static void SetState(this IAbilityControl abilityControl, ControlTypeId controlTypeId, bool value)
        {
            abilityControl.SetState((int)controlTypeId, value);
        }
        
        public static float GetAxis(this IAbilityControl abilityControl, ControlTypeId controlTypeId)
        {
            return abilityControl.GetAxis((int)controlTypeId);
        }
        
        public static Vector2 GetAxis2D(this IAbilityControl abilityControl, ControlTypeId controlTypeId)
        {
            return abilityControl.GetAxis2D((int)controlTypeId);
        }
        
        public static bool GetState(this IAbilityControl abilityControl, ControlTypeId controlTypeId)
        {
            return abilityControl.GetState((int)controlTypeId);
        }
    }
}