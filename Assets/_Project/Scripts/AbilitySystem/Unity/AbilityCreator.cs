using Bnny.Scripts.AbilitySystem.Core;
using Bnny.Scripts.DI;
using VContainer;

namespace Bnny.Scripts.AbilitySystem.Unity
{
    /*
    ⠀⠀⠀⠀⠀⠀⢀⡴⢲⡄⠀⠀⠀⢀⣀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
    ⠀⠀⠀⠀⠀⢀⡞⠀⠀⡇⠀⢀⡴⠋⠁⢸⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
    ⠀⠀⠀⠀⠀⣸⠁⠀⠀⡇⣠⠟⠀⠀⠀⣼⣠⣤⣤⣀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
    ⠀⠀⠀⠀⠀⣿⠀⢠⢤⡿⠃⣀⠀⢀⡞⠉⠁⠀⠀⠈⠙⠶⡄⠀⠀⠀⠀⠀⠀⠀
    ⠀⠀⠀⠀⠀⣻⣰⡏⣼⢁⡴⠋⣰⠋⠀⠀⠀⠀⠀⠀⠀⠀⠹⡄⣀⡀⠀⠀⠀⠀
    ⠀⠀⠀⢀⡾⠋⠉⠁⠀⠙⠁⠞⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢹⡈⢷⠀⠀⠀⠀
    ⠀⠀⠀⣼⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠄⠀⠀⠀⠀⠀⠀⠀⢀⣧⡾⠁⠀⠀⠀
    ⠀⠀⠀⡏⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠄⠀⠀⠀⠀⠀⠀⠀⣾⠃⠀⠀⠀⠀⠀
    ⠀⠀⠀⣹⡄⠀⠀⠀⠀⠀⠀⠀⠀⠀⠘⠁⠀⠀⡀⠀⠀⢰⡾⠋⠀⠀⠀⠀⠀⠀
    ⠀⠀⠀⢻⡇⣀⡀⠀⠺⣿⠇⠀⣀⣤⣄⣀⣠⣬⣥⣤⠾⠛⠁⠀⠀⠀⠀⠀⠀⠀
    ⠀⠀⠀⠀⠉⠛⠓⠂⠤⠤⠖⠊⠉⠉⠉⠉⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
    */
    public class AbilityCreator : IAbilityCreator
    {
        IObjectResolver container;

        public AbilityCreator(IObjectResolver container)
        {
            this.container = container;
        }

        public IAbility CreateAbility<TAbility>(
            params object[] instantiatableClassUnregisteredArguments
        )
            where TAbility : IAbility
        { // Question:
            // is not declaring an interm. variable more efficient?
            return container.Instantiate<TAbility>(instantiatableClassUnregisteredArguments);
        }
    }
}
