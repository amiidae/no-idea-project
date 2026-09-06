using Bnny.Scripts.AbilitySystem.Core;
using Bnny.Scripts.AbilitySystem.Unity;
using UnityEngine;

namespace Bnny.Scripts.AbilitySystem.Abilities.Locomotions
{
    public abstract class LocomotionAbility : Ability
    {
        protected AbilityUser abilityUser;
        protected HeroController heroController;

        protected LocomotionAbility(AbilityUser abilityUser)
        {
            this.abilityUser = abilityUser;
            this.heroController = abilityUser.HeroController;
        }

        public override bool CanBeUsed()
        {
            return heroController.IsGrounded;
        }
    }
}
