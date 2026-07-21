using UnityEngine;

public class Idle : Ability
{
    private HeroContext heroContext;

    public Idle(HeroContext heroContext)
    {
        this.heroContext = heroContext;
    }

    public override bool IsTriggered => heroContext.IsGrounded && heroContext.CurrentSpeed == 0;

    public override bool CanBeDone => heroContext.IsGrounded = true;

    public override void OnEnter()
    {
        Debug.Log("Enter idle");
        heroContext.Animator.Play(heroContext.HashedAnimationName_Idle);
    }

    public override AbilityStatus OnUpdate()
    {
        // move player
        return AbilityStatus.Running;
    }

    public override void OnExit()
    {
        Debug.Log("exit idle");
        // do nothing?
    }
}
