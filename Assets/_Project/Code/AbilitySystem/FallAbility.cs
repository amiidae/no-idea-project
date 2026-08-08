using Code.AbilitySystem.Unity;

namespace Code.AbilitySystem
{
    public class FallAbility : AbilityBase
    {
        private HeroController _heroController;
        private IHeroDataRepository _heroDataRepository;

        public FallAbility(AbilityUser abilityUser, IHeroDataRepository heroDataRepository) : base(abilityUser)
        {
            _heroDataRepository = heroDataRepository;
            _heroController = abilityUser.GetComponent<HeroController>();
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
            float dir = AbilityUser.Blackboard.MoveAxis2D().x;

            float speed = AbilityUser.Blackboard.HasRunInput()
                ? _heroDataRepository.Data.RunSpeed
                : _heroDataRepository.Data.MovementSpeed;

            _heroController.AirMove(dir, speed);
        }
    }
}
