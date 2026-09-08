using Bnny.Scripts.AbilitySystem.Unity;
using Bnny.Scripts.Services.Data;

namespace Bnny.Scripts.AbilitySystem.Abilities.Locomotions
{
    public class RunAbility : LocomotionAbility
    {
        private IDataService dataService;

        public RunAbility(AbilityUser abilityUser, IDataService dataService)
            : base(abilityUser)
        {
            this.dataService = dataService;
        }

        public override bool IsTriggered()
        {
            return abilityUser.AbilityUserBlackboard.GetAxis2D(InputTypeId.Move).x != 0
                && abilityUser.AbilityUserBlackboard.GetState(InputTypeId.Run) == true;
        }

        public override void Use()
        {
            heroController.Animator.Play("Run");
        }

        public override void FixedUpdate()
        {
            float direction = abilityUser.AbilityUserBlackboard.GetAxis2D(InputTypeId.Move).x;
            heroController.Move(
                direction,
                dataService.HeroData.RunSpeed,
                dataService.HeroData.RunSmoothing
            );
        }
    }
}
