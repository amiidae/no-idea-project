using Code.AbilitySystem.Core;

namespace Code.AbilitySystem.Unity
{
    public abstract class AbilityBase : IAbility
    {
        protected AbilityUser AbilityUser { get; }

        public AbilityBase(AbilityUser abilityUser)
        {
            AbilityUser = abilityUser;
        }
        
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