using UnityEngine;

public abstract class LocomotionAbility : Ability
{
    protected IAbilityUserBlackboard abilityUserBlackboard;
    protected HeroController heroController;

    protected LocomotionAbility(
        AbilityUser abilityUser,
        IAbilityUserBlackboard abilityUserBlackboard
    )
    {
        this.abilityUserBlackboard = abilityUserBlackboard;
        this.heroController = abilityUser.HeroController;
    }

    public override bool CanBeUsed()
    {
        return heroController.IsGrounded;
    }
}
