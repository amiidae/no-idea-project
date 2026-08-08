using Code.AbilitySystem.Unity;
using UnityEngine;

namespace Code.AbilitySystem
{
    public class JumpAbility : AbilityBase
    {
        private IInputService _inputService;
        private HeroController _heroController;
        private IHeroDataRepository _heroDataRepository;

        public JumpAbility(AbilityUser abilityUser, IHeroDataRepository heroDataRepository) : base(abilityUser)
        {
            _heroDataRepository = heroDataRepository;
            _heroController = abilityUser.GetComponent<HeroController>();
        }

        public override bool IsTriggered()
        {
            //inputService.hasJumpInput
            return AbilityUser.Blackboard.GetState(ControlTypeId.Jump);
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
            float dir = AbilityUser.Blackboard.MoveAxis2D().x;

            float speed = AbilityUser.Blackboard.HasRunInput()
                ? _heroDataRepository.Data.RunSpeed
                : _heroDataRepository.Data.MovementSpeed;

            _heroController.AirMove(dir, speed);
        }
    }
}
