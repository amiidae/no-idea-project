using System;
using UnityEngine;

public class LongJumpAbility : JumpAbilityBase
{
    private float jumpEndTime;
    private bool mustLand;

    public LongJumpAbility(
        AbilityUser abilityUser,
        IAbilityUserBlackboard abilityUserBlackboard,
        IDataService dataService
    )
        : base(abilityUser, abilityUserBlackboard, dataService) { }

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

    public override void FixedUpdate()
    {
        heroController.LongJump();

        base.FixedUpdate();
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
