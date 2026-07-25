using UnityEngine;

public class IdleAbility : LocomotionAbility
{
    public IdleAbility(AbilityUser abilityUser, IInputService inputService)
        : base(abilityUser, inputService) { }

    public override bool IsTriggered()
    {
        return inputService.MoveAxis.x == 0;
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
