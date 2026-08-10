using Code.AbilitySystem.Unity;

namespace AbilitySystem
{
    public class JumpAbility : JumpAbilityBase
    {
        public JumpAbility(AbilityUser abilityUser, IDataRepository dataRepository) : base(abilityUser, dataRepository)
        {
        }

        public override bool CanBeUsed()
        {
            return _heroController.IsGrounded;
        }
    }
}
