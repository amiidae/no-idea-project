public abstract class Ability : IAbility
{
    public virtual bool IsTriggered
    {
        get { return false; }
    }

    public virtual bool CanBeDone
    {
        get { return true; }
    }

    public virtual void OnEnter() { }

    public virtual AbilityStatus OnUpdate()
    {
        return AbilityStatus.Complete;
    }

    public virtual void OnExit() { }
}
