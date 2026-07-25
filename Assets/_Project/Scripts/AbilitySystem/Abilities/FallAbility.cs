using UnityEngine;

public class FallAbility : Ability
{
    private IInputService inputService;
    private IDataService dataService;
    private HeroController heroController;

    public FallAbility(
        AbilityUser abilityUser,
        IInputService inputService,
        IDataService dataService
    )
    {
        this.inputService = inputService;
        this.dataService = dataService;
        this.heroController = abilityUser.HeroController;
    }

    public override bool IsTriggered()
    {
        return heroController.IsGrounded == false;
    }

    public override void Use()
    {
        heroController.Animator.Play("Fall");
    }

    public override void FixedUpdate()
    {
        float direction = inputService.MoveAxis.x;

        float speed = inputService.IsRunInputReceived
            ? dataService.HeroData.RunSpeed
            : dataService.HeroData.MovementSpeed;

        heroController.AirMove(direction, speed);
    }
}
