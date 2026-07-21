using UnityEngine;

namespace _Project.Scripts.AbilitySystem
{
    public class JumpAbility : Ability
    {
        private IInputService _inputService;
        private HeroController _heroController;
        private IHeroDataRepository _heroDataRepository;

        public JumpAbility(AbilityUser abilityUser, IInputService inputService, IHeroDataRepository heroDataRepository)
        {
            _heroDataRepository = heroDataRepository;
            _heroController = abilityUser.HeroController;
            _inputService = inputService;
        }

        public override bool IsTriggered()
        {
            return _inputService.IsJumpInputReceived;
        }

        public override bool CanBeUsed()
        {
            return _heroController.IsGrounded;
        }

        public override bool CanComplete()
        {
            AnimatorStateInfo state = _heroController.Animator.GetCurrentAnimatorStateInfo(0);
            return state.IsName("Jump") && state.normalizedTime >= 1f;
        }

        public override void Use()
        {
            _heroController.Jump();
            _heroController.Animator.Play("Jump");
        }

        public override void FixedTick()
        {
            float dir = _inputService.MoveAxis.x;

            float speed = _inputService.IsRunInputReceived
                ? _heroDataRepository.Data.RunSpeed
                : _heroDataRepository.Data.MovementSpeed;

            _heroController.AirMove(dir, speed);
        }
    }
}
