using Bnny.Scripts.AbilitySystem.Core;
using Bnny.Scripts.AbilitySystem.Unity;
using UnityEngine;

namespace Bnny.Scripts.AbilitySystem.Abilities.Locomotions
{
    public abstract class LocomotionAbility : Ability
    {
        protected IAbilityUserBlackboard abilityUserBlackboard;
        protected HeroController heroController;

        protected LocomotionAbility(
            AbilityUser abilityUser,
            IAbilityUserBlackboard abilityUserBlackboard
        )
        {
            this.abilityUserBlackboard = abilityUserBlackboard;
            this.heroController = abilityUser.HeroController;
        }

        public override bool CanBeUsed()
        {
            return heroController.IsGrounded;
        }
    }
}
