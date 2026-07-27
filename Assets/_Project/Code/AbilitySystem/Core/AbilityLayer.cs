using System.Collections.Generic;

namespace Code.AbilitySystem.Core
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