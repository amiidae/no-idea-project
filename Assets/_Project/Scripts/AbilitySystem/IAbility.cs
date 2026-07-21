namespace _Project.Scripts.AbilitySystem
{
    public enum AbilityStatus
    {
        Running,
        Completed
    }
    
    public interface IAbility
    {
        void Init();

        bool IsTriggered();

        bool CanBeUsed();
        
        bool CanComplete();

        void Use();
        
        void Tick();

        void FixedTick();

        void Complete();

        void Destroy();
    }

    public abstract class Ability : IAbility
    {
        public virtual bool IsComplete => false;
        
        public virtual void Init() { }

        public virtual bool IsTriggered() => false;

        public virtual bool CanBeUsed() => true;
        
        public virtual bool CanComplete() => false;

        public virtual void Use() { }
        
        public virtual void Tick() { }

        public virtual void FixedTick() { }

        public virtual void Complete() { }

        public virtual void Destroy() { }
    }
}