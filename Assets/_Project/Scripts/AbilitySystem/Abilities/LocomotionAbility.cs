using UnityEngine;

public abstract class LocomotionAbility : Ability
{
    protected IInputService inputService;
    protected HeroController heroController;

    protected LocomotionAbility(AbilityUser abilityUser, IInputService inputService)
    {
        this.inputService = inputService;
        this.heroController = abilityUser.HeroController;
    }

    public override bool CanBeUsed()
    {
        return heroController.IsGrounded;
    }
}
