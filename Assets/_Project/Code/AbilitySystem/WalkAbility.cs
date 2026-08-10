using Code.AbilitySystem.Unity;

namespace Code.AbilitySystem
{
    public class WalkAbility : LocomotionAbility
    {
        private IDataRepository dataRepository;

        public WalkAbility(AbilityUser abilityUser, IDataRepository dataRepository)
            : base(abilityUser)
        {
            this.dataRepository = dataRepository;
        }

        public override bool IsTriggered()
        {
            return AbilityUser.Blackboard.HasMoveInput()
                   && !AbilityUser.Blackboard.HasRunInput();
        }

        public override void Use()
        {
            _heroController.Animator.Play("Walk");
        }

        public override void FixedTick()
        {
            float dir = AbilityUser.Blackboard.MoveAxis2D().x;
            _heroController.Move(dir, dataRepository.HeroData.MovementSpeed, dataRepository.HeroData.WalkSmoothing);
        }
    }
}
