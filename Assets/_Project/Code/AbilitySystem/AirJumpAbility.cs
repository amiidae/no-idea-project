using Code.AbilitySystem.Unity;

namespace AbilitySystem
{
    public class AirJumpAbility : JumpAbilityBase
    {
        private int _airJumpsUsed;

        public AirJumpAbility(AbilityUser abilityUser, IDataRepository dataRepository)
            : base(abilityUser, dataRepository)
        {
        }

        public override void Init()
        {
            _heroController.Landed += ResetAirJumps;
            _heroController.WallJumped += ResetAirJumps;
        }

        public override void Destroy()
        {
            _heroController.Landed -= ResetAirJumps;
            _heroController.WallJumped -= ResetAirJumps;
        }

        public override bool CanBeUsed()
        {
            return !_heroController.IsGrounded
                && _airJumpsUsed < _dataRepository.HeroData.MaxAirJumps;
        }

        public override void Use()
        {
            _airJumpsUsed++;
            base.Use();
        }

        private void ResetAirJumps()
        {
            _airJumpsUsed = 0;
        }
    }
}
