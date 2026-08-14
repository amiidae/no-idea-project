using UnityEngine;

public class WallJumpAbility : JumpAbilityBase
{
    public WallJumpAbility(
        AbilityUser abilityUser,
        IAbilityUserBlackboard abilityUserBlackboard,
        IDataService dataService
    )
        : base(abilityUser, abilityUserBlackboard, dataService) { }

    public override bool IsTriggered()
    {
        return heroController.IsFacedAgainstWall == true
            && abilityUserBlackboard.GetState(InputTypeId.Jump) == true;
        /* player facing the wall == true && jump pressed?*/
    }

    public override bool CanBeUsed()
    {
        return heroController.IsFacedAgainstWall == true && heroController.IsGrounded == false;
    }

    public override void Use()
    {
        heroController.WallJump();

        heroController.Animator.Play("Jump");
    }
}
