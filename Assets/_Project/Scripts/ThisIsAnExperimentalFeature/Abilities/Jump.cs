using UnityEngine;

public class Jump : Ability
{
    HeroContext heroContext;

    public Jump(HeroContext heroContext)
    {
        this.heroContext = heroContext;
    }

    public override bool IsTriggered => heroContext.IsJumpInputReceived;

    public override bool CanBeDone => true;

    public override void OnEnter()
    {
        Debug.Log("enter jump");
        heroContext.Animator.Play(heroContext.HashedAnimationName_JumpStart);
    }

    public override AbilityStatus OnUpdate()
    {
        if (heroContext.Rb.linearVelocityY != 0)
        {
            return AbilityStatus.Running;
        }
        else
        {
            return AbilityStatus.Complete;
        }
    }

    public override void OnExit()
    {
        Debug.Log("exit jump");
    }
}
