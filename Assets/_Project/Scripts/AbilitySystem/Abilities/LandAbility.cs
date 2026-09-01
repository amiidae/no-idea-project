using Bnny.Scripts.AbilitySystem.Unity;
using UnityEngine;

namespace Bnny.Scripts.AbilitySystem.Abilities
{
    public class LandAbility : Ability
    {
        private bool landed;
        private HeroController heroController;

        public LandAbility(AbilityUser abilityUser)
        {
            this.heroController = abilityUser.HeroController;
        }

        public override void Init()
        {
            heroController.Landed += OnLanded;
        }

        public override void Destroy()
        {
            heroController.Landed -= OnLanded;
        }

        public override bool IsTriggered()
        {
            return landed;
        }

        public override bool CanComplete()
        {
            AnimatorStateInfo state = heroController.Animator.GetCurrentAnimatorStateInfo(0);
            return state.IsName("Land") && state.normalizedTime >= 1f;
        }

        public override void Use()
        {
            heroController.Animator.Play("Land");

            landed = false;
        }

        private void OnLanded()
        {
            landed = true;
        }
    }
}
