using Bnny.Scripts.AbilitySystem.Unity;
using Bnny.Scripts.Services.Data;

namespace Bnny.Scripts.AbilitySystem.Abilities.Locomotions
{
    public class WalkAbility : LocomotionAbility
    {
        private IDataService dataService;

        public WalkAbility(AbilityUser abilityUser, IDataService dataService)
            : base(abilityUser)
        {
            this.dataService = dataService;
        }

        public override bool IsTriggered()
        {
            return abilityUser.AbilityUserBlackboard.GetAxis2D(InputTypeId.Move).x != 0
                && abilityUser.AbilityUserBlackboard.GetState(InputTypeId.Run) == false;
        }

        public override void Use()
        {
            heroController.Animator.Play("Walk");
        }

        public override void FixedUpdate()
        {
            float direction = abilityUser.AbilityUserBlackboard.GetAxis2D(InputTypeId.Move).x;
            heroController.Move(
                direction,
                dataService.HeroData.MovementSpeed,
                dataService.HeroData.WalkSmoothing
            );
        }
    }
}
