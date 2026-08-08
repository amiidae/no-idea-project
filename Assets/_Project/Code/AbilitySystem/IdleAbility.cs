using Code.AbilitySystem.Unity;
using UnityEngine;

namespace Code.AbilitySystem
{
    public class IdleAbility : LocomotionAbility
    {
        public IdleAbility(AbilityUser abilityUser) : base(abilityUser)
        {
        }

        public override bool IsTriggered()
        {
            return !AbilityUser.Blackboard.HasMoveInput();
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
