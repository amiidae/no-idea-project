using UnityEngine;

public class IdleAbility : LocomotionAbility
{
    public IdleAbility(AbilityUser abilityUser, IAbilityUserBlackboard abilityUserBlackboard)
        : base(abilityUser, abilityUserBlackboard) { }

    public override bool IsTriggered()
    {
        return abilityUserBlackboard.GetAxis2D(InputTypeId.Move).x == 0;
    }

    public override void Use()
    {
        heroController.Animator.Play("Idle");
    }

    public override void FixedUpdate()
    {
        // ?
        heroController.Move(0f, 0f, 0f);
    }
}
