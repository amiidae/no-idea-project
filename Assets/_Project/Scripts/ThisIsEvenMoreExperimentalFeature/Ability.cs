public abstract class Ability : IAbility
{
    public virtual void Init() { }

    public virtual bool IsTriggered()
    {
        return true;
    }

    public virtual bool CanBeUsed()
    {
        return true;
    }

    public virtual bool CanComplete()
    {
        return true;
    }

    public virtual void Use() { }

    public virtual void Update() { }

    public virtual void FixedUpdate() { }

    public virtual void Complete() { }

    public virtual void Destroy() { }
}
