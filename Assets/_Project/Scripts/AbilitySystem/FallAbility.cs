namespace _Project.Scripts.AbilitySystem
{
    public class FallAbility : Ability
    {
        private HeroController _heroController;
        private IHeroDataRepository _heroDataRepository;
        private IInputService _inputService;

        public FallAbility(AbilityUser abilityUser, IInputService inputService, IHeroDataRepository heroDataRepository)
        {
            _inputService = inputService;
            _heroDataRepository = heroDataRepository;
            _heroController = abilityUser.HeroController;
        }

        public override bool IsTriggered()
        {
            return !_heroController.IsGrounded;
        }

        public override void Use()
        {
            _heroController.Animator.Play("Fall");
        }

        public override void FixedTick()
        {
            float dir = _inputService.MoveAxis.x;

            float speed = _inputService.IsRunInputReceived
                ? _heroDataRepository.Data.RunSpeed
                : _heroDataRepository.Data.MovementSpeed;

            _heroController.AirMove(dir, speed);
        }
    }
}
