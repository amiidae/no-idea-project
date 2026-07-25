using UnityEngine;

public class RunAbility : LocomotionAbility
{
    private IDataService dataService;

    public RunAbility(AbilityUser abilityUser, IInputService inputService, IDataService dataService)
        : base(abilityUser, inputService)
    {
        this.dataService = dataService;
    }

    public override bool IsTriggered()
    {
        return inputService.MoveAxis.x != 0 && inputService.IsRunInputReceived == true;
    }

    public override void Use()
    {
        heroController.Animator.Play("Run");
    }

    public override void FixedUpdate()
    {
        float direction = inputService.MoveAxis.x;
        heroController.Move(
            direction,
            dataService.HeroData.RunSpeed,
            dataService.HeroData.RunSmoothing
        );
    }
}
