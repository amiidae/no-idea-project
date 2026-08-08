namespace Code.AbilitySystem.Core
{
    public interface IAbility
    {
        void Init();

        void Destroy();
        
        bool IsTriggered();

        bool CanBeUsed();
        
        void Use();

        bool CanComplete();
        
        void Tick();

        void FixedTick();

        void Complete();

    }
}