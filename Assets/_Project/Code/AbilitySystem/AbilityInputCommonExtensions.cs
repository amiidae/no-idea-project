using Code.AbilitySystem.Core;
using UnityEngine;

namespace Code.AbilitySystem
{
    public static class AbilityInputCommonExtensions
    {
        public static bool HasRunInput(this IAbilityBlackboard abilityBlackboard)
        {
            return abilityBlackboard.GetState(ControlTypeId.Run);
        }
        
        public static bool HasJumpInput(this IAbilityBlackboard abilityBlackboard)
        {
            return abilityBlackboard.GetState(ControlTypeId.Jump);
        }

        public static bool HasMoveInput(this IAbilityBlackboard abilityBlackboard)
        {
            return abilityBlackboard.MoveAxis2D().x != 0;
        }
        
        public static Vector2 MoveAxis2D(this IAbilityBlackboard abilityBlackboard)
        {
            return abilityBlackboard.GetAxis2D(ControlTypeId.Move);
        }
    }
}