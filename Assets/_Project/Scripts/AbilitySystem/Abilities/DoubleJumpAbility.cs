using UnityEngine;

public class DoubleJumpAbility : Ability
{
    private IAbilityUserBlackboard abilityUserBlackboard;
    private IDataService dataService;
    private HeroController heroController;

    public DoubleJumpAbility(
        AbilityUser abilityUser,
        IAbilityUserBlackboard abilityUserBlackboard,
        IDataService dataService
    )
    {
        this.abilityUserBlackboard = abilityUserBlackboard;
        this.dataService = dataService;
        this.heroController = abilityUser.HeroController;
    }

    public override bool IsTriggered()
    {
        return heroController.IsGrounded == false
            && abilityUserBlackboard.GetState((int)InputTypeId.Jump);
    }

    public override bool CanBeUsed()
    {
        return heroController.NumberOfJumpsLeft > 0;
    }

    public override bool CanComplete()
    {
        AnimatorStateInfo state = heroController.Animator.GetCurrentAnimatorStateInfo(0);
        return state.IsName("Jump") && state.normalizedTime >= 1f;
    }

    public override void Use()
    {
        heroController.Jump();

        heroController.Animator.Play("Jump");
    }

    public override void FixedUpdate()
    {
        float direction = abilityUserBlackboard.GetAxis2D((int)InputTypeId.Move).x;

        float speed = abilityUserBlackboard.GetState((int)InputTypeId.Run)
            ? dataService.HeroData.RunSpeed
            : dataService.HeroData.MovementSpeed;

        heroController.AirMove(direction, speed);
    }
}
