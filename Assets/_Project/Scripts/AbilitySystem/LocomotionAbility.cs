namespace _Project.Scripts.AbilitySystem
{
    public abstract class LocomotionAbility : Ability
    {
        protected IInputService _inputService;
        protected HeroController _heroController;

        protected LocomotionAbility(AbilityUser abilityUser, IInputService inputService)
        {
            _heroController = abilityUser.HeroController;
            _inputService = inputService;
        }

        public override bool CanBeUsed()
        {
            return _heroController.IsGrounded;
        }
    }
}
