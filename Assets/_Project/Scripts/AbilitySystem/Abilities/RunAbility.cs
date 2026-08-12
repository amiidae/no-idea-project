using UnityEngine;

public class RunAbility : LocomotionAbility
{
    private IDataService dataService;

    public RunAbility(
        AbilityUser abilityUser,
        IAbilityUserBlackboard abilityUserBlackboard,
        IDataService dataService
    )
        : base(abilityUser, abilityUserBlackboard)
    {
        this.dataService = dataService;
    }

    public override bool IsTriggered()
    {
        return abilityUserBlackboard.GetAxis2D(InputTypeId.Move).x != 0
            && abilityUserBlackboard.GetState(InputTypeId.Run) == true;
    }

    public override void Use()
    {
        heroController.Animator.Play("Run");
    }

    public override void FixedUpdate()
    {
        float direction = abilityUserBlackboard.GetAxis2D(InputTypeId.Move).x;
        heroController.Move(
            direction,
            dataService.HeroData.RunSpeed,
            dataService.HeroData.RunSmoothing
        );
    }
}
