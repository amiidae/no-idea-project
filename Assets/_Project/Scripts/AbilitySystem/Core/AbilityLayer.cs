using System.Collections.Generic;
using System.Linq;

namespace Bnny.Scripts.AbilitySystem.Core
{
    public class AbilityLayer
    {
        public IAbility ActiveAbility;
        public IReadOnlyList<IAbility> Abilities { get; private set; }

        public AbilityLayer(params IAbility[] abilities)
        {
            this.Abilities = abilities.ToList();
        }
    }
}
