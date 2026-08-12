using System;
using UnityEngine;

public class LongJumpAbility : Ability
{
    private IAbilityUserBlackboard abilityUserBlackboard;
    private IDataService dataService;
    private HeroController heroController;

    private float jumpEndTime;
    private bool mustLand;

    public LongJumpAbility(
        AbilityUser abilityUser,
        IAbilityUserBlackboard abilityUserBlackboard,
        IDataService dataService
    )
    {
        this.abilityUserBlackboard = abilityUserBlackboard;
        this.dataService = dataService;
        this.heroController = abilityUser.HeroController;
    }

    public override void Init()
    {
        heroController.Landed += OnLanded;
    }

    public override bool IsTriggered()
    {
        return abilityUserBlackboard.GetState(InputTypeId.LongJump);
    }

    public override bool CanBeUsed()
    {
        return heroController.IsGrounded == false && mustLand == false;
    }

    public override bool CanComplete()
    {
        return abilityUserBlackboard.GetState(InputTypeId.LongJump) == false
            || jumpEndTime < Time.time;
    }

    public override void Use()
    {
        SetJumpEndTime();

        heroController.Animator.Play("Fall");
    }

    public override void Update()
    {
        heroController.LongJump();
    }

    public override void FixedUpdate()
    {
        float direction = abilityUserBlackboard.GetAxis2D(InputTypeId.Move).x;

        float speed = abilityUserBlackboard.GetState(InputTypeId.Run)
            ? dataService.HeroData.RunSpeed
            : dataService.HeroData.MovementSpeed;

        heroController.AirMove(direction, speed);
    }

    public override void Complete()
    {
        mustLand = true;
    }

    public override void Destroy()
    {
        heroController.Landed -= OnLanded;
    }

    private void SetJumpEndTime()
    {
        jumpEndTime = Time.time + dataService.HeroData.MaxJumpDuration;
    }

    private void OnLanded()
    {
        mustLand = false;
    }
}
