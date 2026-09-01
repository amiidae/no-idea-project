using Bnny.Scripts.AbilitySystem.Core;
using UnityEngine;

namespace Bnny.Scripts.AbilitySystem.Unity
{
    public static class MonkeyPatches
    {
        public static float GetAxis(
            this IAbilityUserBlackboard abilityUserBlackboard,
            InputTypeId inputTypeId
        )
        {
            return abilityUserBlackboard.GetAxis((int)inputTypeId);
        }

        public static Vector2 GetAxis2D(
            this IAbilityUserBlackboard abilityUserBlackboard,
            InputTypeId inputTypeId
        )
        {
            return abilityUserBlackboard.GetAxis2D((int)inputTypeId);
        }

        public static bool GetState(
            this IAbilityUserBlackboard abilityUserBlackboard,
            InputTypeId inputTypeId
        )
        {
            return abilityUserBlackboard.GetState((int)inputTypeId);
        }

        public static void SetAxis(
            this IAbilityUserBlackboard abilityUserBlackboard,
            InputTypeId inputTypeId,
            float value
        )
        {
            abilityUserBlackboard.SetAxis((int)inputTypeId, value);
        }

        public static void SetAxis2D(
            this IAbilityUserBlackboard abilityUserBlackboard,
            InputTypeId inputTypeId,
            Vector2 value
        )
        {
            abilityUserBlackboard.SetAxis2D((int)inputTypeId, value);
        }

        public static void SetState(
            this IAbilityUserBlackboard abilityUserBlackboard,
            InputTypeId inputTypeId,
            bool value
        )
        {
            abilityUserBlackboard.SetState((int)inputTypeId, value);
        }
    }
}
