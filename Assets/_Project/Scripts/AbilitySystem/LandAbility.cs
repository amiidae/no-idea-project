using UnityEngine;

namespace _Project.Scripts.AbilitySystem
{
    public class LandAbility : Ability
    {
        private bool _landed;
        private HeroController _heroController;

        public LandAbility(AbilityUser abilityUser)
        {
            _heroController = abilityUser.HeroController;
        }

        public override void Init()
        {
            _heroController.Landed += OnLanded;
        }

        public override void Destroy()
        {
            _heroController.Landed -= OnLanded;
        }

        public override bool CanComplete()
        {
            AnimatorStateInfo state = _heroController.Animator.GetCurrentAnimatorStateInfo(0);
            return state.IsName("Land") && state.normalizedTime >= 1f;
        }

        public override bool IsTriggered()
        {
            return _landed;
        }

        public override void Use()
        {
            _landed = false;
            _heroController.Animator.Play("Land");
        }

        private void OnLanded()
        {
            _landed = true;
        }
    }
}
