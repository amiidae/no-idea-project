using Bnny.Scripts.AbilitySystem.Core;
using Bnny.Scripts.AbilitySystem.Unity;
using Bnny.Scripts.Services.Data;
using UnityEngine;

namespace Bnny.Scripts.AbilitySystem.Abilities.Jumps
{
    public class JumpAbility : JumpAbilityBase
    {
        public JumpAbility(
            AbilityUser abilityUser,
            IAbilityUserBlackboard abilityUserBlackboard,
            IDataService dataService
        )
            : base(abilityUser, abilityUserBlackboard, dataService) { }

        public override bool CanBeUsed()
        {
            return heroController.IsGrounded == true;
        }
    }
}
