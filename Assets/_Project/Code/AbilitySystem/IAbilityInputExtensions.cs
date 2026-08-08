using Code.AbilitySystem.Core;
using UnityEngine;

namespace Code.AbilitySystem
{
    public static class AbilityInputControlTypeExtensions
    {
        public static void SetAxis(this IAbilityBlackboard abilityBlackboard, ControlTypeId controlTypeId, float value)
        {
            abilityBlackboard.SetAxis((int)controlTypeId, value);
        }
        
        public static void SetAxis2D(this IAbilityBlackboard abilityBlackboard, ControlTypeId controlTypeId, Vector2 value)
        {
            abilityBlackboard.SetAxis2D((int)controlTypeId, value);
        }
        
        public static void SetState(this IAbilityBlackboard abilityBlackboard, ControlTypeId controlTypeId, bool value)
        {
            abilityBlackboard.SetState((int)controlTypeId, value);
        }
        
        public static float GetAxis(this IAbilityBlackboard abilityBlackboard, ControlTypeId controlTypeId)
        {
            return abilityBlackboard.GetAxis((int)controlTypeId);
        }
        
        public static Vector2 GetAxis2D(this IAbilityBlackboard abilityBlackboard, ControlTypeId controlTypeId)
        {
            return abilityBlackboard.GetAxis2D((int)controlTypeId);
        }
        
        public static bool GetState(this IAbilityBlackboard abilityBlackboard, ControlTypeId controlTypeId)
        {
            return abilityBlackboard.GetState((int)controlTypeId);
        }
    }
}