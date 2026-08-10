using Code.AbilitySystem.Unity;

namespace Code.AbilitySystem
{
    public class FallAbility : AbilityBase
    {
        private HeroController _heroController;
        private IDataRepository dataRepository;

        public FallAbility(AbilityUser abilityUser, IDataRepository dataRepository) : base(abilityUser)
        {
            this.dataRepository = dataRepository;
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
                ? dataRepository.HeroData.RunSpeed
                : dataRepository.HeroData.MovementSpeed;

            _heroController.AirMove(dir, speed);
        }
    }
}
