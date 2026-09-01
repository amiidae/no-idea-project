using Bnny.Scripts.AbilitySystem.Core;
using Bnny.Scripts.AbilitySystem.Unity;
using Bnny.Scripts.Services.Data;

namespace Bnny.Scripts.AbilitySystem.Abilities
{
    public class FallAbility : Ability
    {
        private IAbilityUserBlackboard abilityUserBlackboard;
        private IDataService dataService;
        private HeroController heroController;

        public FallAbility(
            AbilityUser abilityUser,
            IAbilityUserBlackboard abilityUserBlackboard,
            IDataService dataService
        )
        {
            this.abilityUserBlackboard = abilityUserBlackboard;
            this.dataService = dataService;
            this.heroController = abilityUser.HeroController;
        }

        public override bool IsTriggered()
        {
            return heroController.IsGrounded == false;
        }

        public override void Use()
        {
            heroController.Animator.Play("Fall");
        }

        public override void FixedUpdate()
        {
            float direction = abilityUserBlackboard.GetAxis2D(InputTypeId.Move).x;

            float speed = abilityUserBlackboard.GetState(InputTypeId.Run)
                ? dataService.HeroData.RunSpeed
                : dataService.HeroData.MovementSpeed;

            heroController.AirMove(direction, speed);
        }
    }
}
