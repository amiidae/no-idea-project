using Bnny.Scripts.AbilitySystem.Unity;
using Bnny.Scripts.Services.Data;

namespace Bnny.Scripts.AbilitySystem.Abilities
{
    public class FallAbility : Ability
    {
        private AbilityUser abilityUser;
        private IDataService dataService;
        private HeroController heroController;

        public FallAbility(AbilityUser abilityUser, IDataService dataService)
        {
            this.abilityUser = abilityUser;
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
            float direction = abilityUser.AbilityUserBlackboard.GetAxis2D(InputTypeId.Move).x;

            float speed = abilityUser.AbilityUserBlackboard.GetState(InputTypeId.Run)
                ? dataService.HeroData.RunSpeed
                : dataService.HeroData.MovementSpeed;

            heroController.AirMove(direction, speed);
        }
    }
}
