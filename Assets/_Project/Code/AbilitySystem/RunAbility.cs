using Code.AbilitySystem.Unity;

namespace Code.AbilitySystem
{
    public class RunAbility : LocomotionAbility
    {
        private IDataRepository dataRepository;

        public RunAbility(AbilityUser abilityUser, IDataRepository dataRepository)
            : base(abilityUser)
        {
            this.dataRepository = dataRepository;
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
            _heroController.Move(dir, dataRepository.HeroData.RunSpeed, dataRepository.HeroData.RunSmoothing);
        }
    }
}
