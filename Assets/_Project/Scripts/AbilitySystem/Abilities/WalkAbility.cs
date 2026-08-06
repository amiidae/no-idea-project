using UnityEngine;

public class WalkAbility : LocomotionAbility
{
    private IDataService dataService;

    public WalkAbility(
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
        return abilityUserBlackboard.GetAxis2D((int)InputTypeId.Move).x != 0
            && abilityUserBlackboard.GetState((int)InputTypeId.Run) == false;
    }

    public override void Use()
    {
        heroController.Animator.Play("Walk");
    }

    public override void FixedUpdate()
    {
        float direction = abilityUserBlackboard.GetAxis2D((int)InputTypeId.Move).x;
        heroController.Move(
            direction,
            dataService.HeroData.MovementSpeed,
            dataService.HeroData.WalkSmoothing
        );
    }
}
