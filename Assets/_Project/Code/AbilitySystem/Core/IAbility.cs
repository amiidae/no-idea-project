namespace Code.AbilitySystem.Core
{
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
}