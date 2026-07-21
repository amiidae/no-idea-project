using UnityEngine;

public class Walk : Ability
{
    private HeroContext heroContext;

    public Walk(HeroContext heroContext)
    {
        this.heroContext = heroContext;
    }

    public override bool IsTriggered => heroContext.IsGrounded && heroContext.CurrentSpeed == 5;

    public override bool CanBeDone => heroContext.IsGrounded;

    public override void OnEnter()
    {
        Debug.Log("enter walk");
        heroContext.Animator.Play(heroContext.HashedAnimationName_Walk);
    }

    public override AbilityStatus OnUpdate()
    {
        return AbilityStatus.Running;
    }

    public override void OnExit()
    {
        Debug.Log("exit walk");
    }
}
