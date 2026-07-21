using UnityEngine;

namespace _Project.Scripts.AbilitySystem
{
    public class IdleAbility : LocomotionAbility
    {
        public IdleAbility(AbilityUser abilityUser, IInputService inputService)
            : base(abilityUser, inputService)
        {
        }

        public override bool IsTriggered()
        {
            return _inputService.MoveAxis.x == 0;
        }

        public override void Use()
        {
            _heroController.Animator.Play("Idle");
        }

        public override void FixedTick()
        {
            _heroController.Move(0f, 0f, 0f);
        }
    }
}
