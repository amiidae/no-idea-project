using UnityEngine;

public class WalkAbility : LocomotionAbility
{
    private IDataService dataService;

    public WalkAbility(
        AbilityUser abilityUser,
        IInputService inputService,
        IDataService dataService
    )
        : base(abilityUser, inputService)
    {
        this.dataService = dataService;
    }

    public override bool IsTriggered()
    {
        return inputService.MoveAxis.x != 0 && inputService.IsRunInputReceived == false;
    }

    public override void Use()
    {
        heroController.Animator.Play("Walk");
    }

    public override void FixedUpdate()
    {
        float direction = inputService.MoveAxis.x;
        heroController.Move(
            direction,
            dataService.HeroData.MovementSpeed,
            dataService.HeroData.WalkSmoothing
        );
    }
}
