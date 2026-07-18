using UnityEngine;

namespace _Project.Scripts.AbilitySystem
{
    public class IdleAbility : Ability
    {
        private IInputService _inputService;
        private HeroController _heroController;

        public IdleAbility(HeroController heroController, IInputService inputService)
        {
            _heroController = heroController;
            _inputService = inputService;
        }
        
        public override bool IsTriggered()
        {
            return _inputService.MoveAxis.x == 0;
        }

        public override void OnUse()
        {
            _heroController.Animator.Play("Idle");
        }
    }
}