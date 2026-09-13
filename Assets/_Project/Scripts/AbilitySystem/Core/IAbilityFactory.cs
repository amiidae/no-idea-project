namespace Bnny.Scripts.AbilitySystem.Core
{
    public interface IAbilityFactory
    {
        public IAbility CreateAbility<TAbility>(
            params object[] instantiatableClassUnregisteredArguments
        )
            where TAbility : IAbility;
    }
}
