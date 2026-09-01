using Bnny.Scripts.AbilitySystem.Core;
using Bnny.Scripts.AbilitySystem.Unity;
using Bnny.Scripts.Services.Data;
using UnityEngine;

namespace Bnny.Scripts.AbilitySystem.Abilities.Jumps
{
    public abstract class JumpAbilityBase : Ability
    {
        protected IAbilityUserBlackboard abilityUserBlackboard;
        protected IDataService dataService;
        protected HeroController heroController;

        public JumpAbilityBase(
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
            return abilityUserBlackboard.GetState(InputTypeId.Jump);
        }

        public override bool CanComplete()
        {
            AnimatorStateInfo state = heroController.Animator.GetCurrentAnimatorStateInfo(0);
            return state.IsName("Jump") && state.normalizedTime >= 1f;
        }

        public override void Use()
        {
            heroController.Jump();

            heroController.Animator.Play("Jump");
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
