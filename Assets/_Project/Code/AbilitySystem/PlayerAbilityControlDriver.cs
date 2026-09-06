using Code.AbilitySystem.Core;
using UnityEngine;
using VContainer;

namespace Code.AbilitySystem
{
    public class PlayerAbilityControlDriver : MonoBehaviour
    {
        private IInputService _inputService;
        private IAbilityUser _abilityUser;

        [Inject]
        public void Construct(IInputService inputService)
        {
            _inputService = inputService;
        }

        private void Start()
        {
            _abilityUser = GetComponent<IAbilityUser>();
        }

        private void Update()
        {
            SetInputs();
        }

        private void SetInputs()
        {
            _abilityUser.Blackboard.SetAxis2D(ControlTypeId.Move, _inputService.MoveAxis);
            
            _abilityUser.Blackboard.SetState(ControlTypeId.Jump, _inputService.IsJumpInputReceived);

            _abilityUser.Blackboard.SetState(ControlTypeId.LongJump, _inputService.IsLongJumpInputReceived);

            _abilityUser.Blackboard.SetState(ControlTypeId.Run, _inputService.IsRunInputReceived);
        }
    }
}