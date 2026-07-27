using Code.AbilitySystem.Unity;

namespace Code.AbilitySystem
{
    public abstract class LocomotionAbility : AbilityBase
    {
        protected IInputService _inputService;
        protected HeroController _heroController;

        protected LocomotionAbility(AbilityUser abilityUser) : base(abilityUser)
        {
            _heroController = abilityUser.GetComponent<HeroController>();
        }

        public override bool CanBeUsed()
        {
            return _heroController.IsGrounded;
        }
    }
}
