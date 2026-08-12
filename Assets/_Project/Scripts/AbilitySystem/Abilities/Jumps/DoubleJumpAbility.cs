using UnityEngine;

public class DoubleJumpAbility : JumpAbilityBase
{
    public DoubleJumpAbility(
        AbilityUser abilityUser,
        IAbilityUserBlackboard abilityUserBlackboard,
        IDataService dataService
    )
        : base(abilityUser, abilityUserBlackboard, dataService) { }

    public override bool IsTriggered()
    {
        return base.IsTriggered() && heroController.IsGrounded == false;
    }

    public override bool CanBeUsed()
    {
        return heroController.NumberOfJumpsLeft > 0;
    }
}
