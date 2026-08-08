using Code.AbilitySystem.Unity;

namespace Code.AbilitySystem
{
    public class WalkAbility : LocomotionAbility
    {
        private IHeroDataRepository _heroDataRepository;

        public WalkAbility(AbilityUser abilityUser, IHeroDataRepository heroDataRepository)
            : base(abilityUser)
        {
            _heroDataRepository = heroDataRepository;
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
            _heroController.Move(dir, _heroDataRepository.Data.MovementSpeed, _heroDataRepository.Data.WalkSmoothing);
        }
    }
}
