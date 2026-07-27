using Code.AbilitySystem.Core;
using UnityEngine;

namespace Code.AbilitySystem
{
    public static class AbilityInputCommonExtensions
    {
        public static bool HasRunInput(this IAbilityControl abilityControl)
        {
            return abilityControl.GetState(ControlTypeId.Run);
        }
        
        public static bool HasJumpInput(this IAbilityControl abilityControl)
        {
            return abilityControl.GetState(ControlTypeId.Jump);
        }

        public static bool HasMoveInput(this IAbilityControl abilityControl)
        {
            return abilityControl.MoveAxis2D().x != 0;
        }
        
        public static Vector2 MoveAxis2D(this IAbilityControl abilityControl)
        {
            return abilityControl.GetAxis2D(ControlTypeId.Move);
        }
    }
}