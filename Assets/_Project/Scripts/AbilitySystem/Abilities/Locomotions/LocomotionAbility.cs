using Bnny.Scripts.AbilitySystem.Unity;

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
