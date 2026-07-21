using UnityEngine;

public class Run : Ability
{
    private HeroContext heroContext;

    public Run(HeroContext heroContext)
    {
        this.heroContext = heroContext;
    }

    public override bool IsTriggered =>
        heroContext.IsGrounded && heroContext.CurrentSpeed == (5 * 2.5);

    public override bool CanBeDone => heroContext.IsGrounded;

    public override void OnEnter()
    {
        Debug.Log("enter run");
        heroContext.Animator.Play(heroContext.HashedAnimationName_Run);
    }

    public override AbilityStatus OnUpdate()
    {
        return AbilityStatus.Running;
    }

    public override void OnExit()
    {
        Debug.Log("exit run");
    }
}
