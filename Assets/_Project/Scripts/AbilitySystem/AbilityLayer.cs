using System.Collections.Generic;

namespace _Project.Scripts.AbilitySystem
{
    public class AbilityLayer
    {
        public IAbility ActiveAbility { get; set; }

        public IReadOnlyList<IAbility> Abilities { get; private set; }
        

        public AbilityLayer(params IAbility[] abilities)
        {
            Abilities = abilities;
        }
    }
}