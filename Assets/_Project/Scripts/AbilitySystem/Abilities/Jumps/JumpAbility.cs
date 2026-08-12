using UnityEngine;

public class JumpAbility : JumpAbilityBase
{
    public JumpAbility(
        AbilityUser abilityUser,
        IAbilityUserBlackboard abilityUserBlackboard,
        IDataService dataService
    )
        : base(abilityUser, abilityUserBlackboard, dataService) { }

    public override bool CanBeUsed()
    {
        return heroController.IsGrounded == true;
    }
}
