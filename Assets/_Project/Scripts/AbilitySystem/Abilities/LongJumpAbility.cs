using System;
using UnityEngine;

public class LongJumpAbility : Ability
{
    private IInputService inputService;
    private IDataService dataService;
    private HeroController heroController;

    private float jumpEndTime;
    private bool mustLand;

    public LongJumpAbility(
        AbilityUser abilityUser,
        IInputService inputService,
        IDataService dataService
    )
    {
        this.inputService = inputService;
        this.dataService = dataService;
        this.heroController = abilityUser.HeroController;
    }

    public override void Init()
    {
        heroController.Landed += OnLanded;
    }

    public override bool IsTriggered()
    {
        return inputService.IsJumpInputContinuous;
    }

    public override bool CanBeUsed()
    {
        Debug.Log(
            $"long jump can be used: {heroController.IsGrounded == false && mustLand == false}"
        );
        return heroController.IsGrounded == false && mustLand == false;
    }

    public override bool CanComplete()
    {
        Debug.Log(
            $"long jump can complete: {inputService.IsJumpInputContinuous == false || jumpEndTime < Time.time}"
        );
        return inputService.IsJumpInputContinuous == false || jumpEndTime < Time.time;
    }

    public override void Use()
    {
        Debug.Log("use long jump");

        SetJumpEndTime();

        heroController.Animator.Play("Fall");
    }

    public override void Update()
    {
        heroController.LongJump();
    }

    public override void FixedUpdate()
    {
        float direction = inputService.MoveAxis.x;

        float speed = inputService.IsRunInputReceived
            ? dataService.HeroData.RunSpeed
            : dataService.HeroData.MovementSpeed;

        heroController.AirMove(direction, speed);
    }

    public override void Complete()
    {
        Debug.Log("fin long jump");
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
        Debug.Log("on landed long jump");
        mustLand = false;
    }
}
