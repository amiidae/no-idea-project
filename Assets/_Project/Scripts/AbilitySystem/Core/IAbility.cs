namespace Bnny.Scripts.AbilitySystem.Core
{
    public interface IAbility
    {
        public void Init();
        public bool IsTriggered();
        public bool CanBeUsed();
        public bool CanComplete();
        public void Use();
        public void Update();
        public void FixedUpdate();
        public void Complete();
        public void Destroy();
    }
}
