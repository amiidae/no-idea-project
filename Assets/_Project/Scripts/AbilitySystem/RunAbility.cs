using UnityEngine;

namespace _Project.Scripts.AbilitySystem
{
    public class RunAbility : LocomotionAbility
    {
        private IHeroDataRepository _heroDataRepository;

        public RunAbility(AbilityUser abilityUser, IInputService inputService, IHeroDataRepository heroDataRepository)
            : base(abilityUser, inputService)
        {
            _heroDataRepository = heroDataRepository;
        }

        public override bool IsTriggered()
        {
            return _inputService.MoveAxis.x != 0 && _inputService.IsRunInputReceived;
        }

        public override void Use()
        {
            _heroController.Animator.Play("Run");
        }

        public override void FixedTick()
        {
            float dir = _inputService.MoveAxis.x;
            _heroController.Move(dir, _heroDataRepository.Data.RunSpeed, _heroDataRepository.Data.RunSmoothing);
        }
    }
}
