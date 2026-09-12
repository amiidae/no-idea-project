namespace Bnny.Scripts.AbilitySystem.Core
{
    public interface IAbilityCreator
    {
        public IAbility CreateAbility<TAbility>(
            params object[] instantiatableClassUnregisteredArguments
        )
            where TAbility : IAbility;
    }
}
