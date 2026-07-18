namespace _Project.Scripts.AbilitySystem
{
    public enum AbilityStatus
    {
        Running,
        Completed
    }
    
    public interface IAbility
    {
        // 
        bool IsTriggered(); 

        // 
        bool CanBeUsed();
        
        // 
        void OnUse();
        
        AbilityStatus OnPerform();
        
        void OnComplete();
    }

    public abstract class Ability : IAbility
    {
        public virtual bool IsTriggered() => false;

        public virtual bool CanBeUsed() => true;

        public virtual void OnUse() { }

        public virtual AbilityStatus OnPerform() => AbilityStatus.Completed;

        public virtual void OnComplete() { }
    }
}