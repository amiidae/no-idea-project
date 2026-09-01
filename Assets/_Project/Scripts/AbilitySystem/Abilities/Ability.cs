using Bnny.Scripts.AbilitySystem.Core;

namespace Bnny.Scripts.AbilitySystem.Abilities
{
    public abstract class Ability : IAbility
    {
        public virtual void Init() { }

        public virtual bool IsTriggered()
        {
            return false;
        }

        public virtual bool CanBeUsed()
        {
            return true;
        }

        public virtual bool CanComplete()
        {
            return false;
        }

        public virtual void Use() { }

        public virtual void Update() { }

        public virtual void FixedUpdate() { }

        public virtual void Complete() { }

        public virtual void Destroy() { }
    }
}
