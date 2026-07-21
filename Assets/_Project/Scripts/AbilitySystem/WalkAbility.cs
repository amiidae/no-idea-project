using UnityEngine;

namespace _Project.Scripts.AbilitySystem
{
    public class WalkAbility : LocomotionAbility
    {
        private IHeroDataRepository _heroDataRepository;

        public WalkAbility(AbilityUser abilityUser, IInputService inputService, IHeroDataRepository heroDataRepository)
            : base(abilityUser, inputService)
        {
            _heroDataRepository = heroDataRepository;
        }

        public override bool IsTriggered()
        {
            return _inputService.MoveAxis.x != 0 && !_inputService.IsRunInputReceived;
        }

        public override void Use()
        {
            _heroController.Animator.Play("Walk");
        }

        public override void FixedTick()
        {
            float dir = _inputService.MoveAxis.x;
            _heroController.Move(dir, _heroDataRepository.Data.MovementSpeed, _heroDataRepository.Data.WalkSmoothing);
        }
    }
}
