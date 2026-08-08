using Code.AbilitySystem.Unity;

namespace Code.AbilitySystem
{
    public class RunAbility : LocomotionAbility
    {
        private IHeroDataRepository _heroDataRepository;

        public RunAbility(AbilityUser abilityUser, IHeroDataRepository heroDataRepository)
            : base(abilityUser)
        {
            _heroDataRepository = heroDataRepository;
        }

        public override bool IsTriggered()
        {
            return AbilityUser.Blackboard.HasMoveInput() && AbilityUser.Blackboard.HasRunInput();
        }

        public override void Use()
        {
            _heroController.Animator.Play("Run");
        }

        public override void FixedTick()
        {
            float dir = AbilityUser.Blackboard.MoveAxis2D().x;
            _heroController.Move(dir, _heroDataRepository.Data.RunSpeed, _heroDataRepository.Data.RunSmoothing);
        }
    }
}
