using Code.AbilitySystem;
using Code.AbilitySystem.Unity;
using UnityEngine;

namespace AbilitySystem
{
    public abstract class JumpAbilityBase : AbilityBase
    {
        protected HeroController _heroController;
        protected IDataRepository _dataRepository;

        protected JumpAbilityBase(AbilityUser abilityUser, IDataRepository dataRepository) : base(abilityUser)
        {
            _heroController = abilityUser.GetComponent<HeroController>();
            _dataRepository = dataRepository;
        }

        public override bool IsTriggered()
        {
            return AbilityUser.Blackboard.GetState(ControlTypeId.Jump);
        }

        public override void Use()
        {
            _heroController.Jump();
            _heroController.Animator.Play("Jump");
        }

        public override bool CanComplete()
        {
            if (!AbilityUser.Blackboard.GetState(ControlTypeId.LongJump))
            {
                return true;
            }

            AnimatorStateInfo state = _heroController.Animator.GetCurrentAnimatorStateInfo(0);
            return state.IsName("Jump") && state.normalizedTime >= 1f;
        }

        public override void FixedTick()
        {
            float dir = AbilityUser.Blackboard.MoveAxis2D().x;

            float speed = AbilityUser.Blackboard.HasRunInput()
                ? _dataRepository.HeroData.RunSpeed
                : _dataRepository.HeroData.MovementSpeed;

            _heroController.AirMove(dir, speed);

            _heroController.ApplyJumpAcceleration();
        }
    }
}
