using Code.AbilitySystem.Core;
using UnityEngine;

namespace Code.AbilitySystem
{
    public class PlayerAbilityControlDriver : MonoBehaviour
    {
        private IInputService _inputService;
        private IAbilityUser _abilityUser;

        private void Start()
        {
            _inputService = ServiceLocator.GetService<IInputService>();
            _abilityUser = GetComponent<IAbilityUser>();
        }

        private void Update()
        {
            SetInputs();
        }

        private void SetInputs()
        {
            _abilityUser.Control.SetAxis2D(ControlTypeId.Move, _inputService.MoveAxis);
            
            _abilityUser.Control.SetState(ControlTypeId.Jump, _inputService.IsJumpInputReceived);
            _abilityUser.Control.SetState(ControlTypeId.Run, _inputService.IsRunInputReceived);
        }
    }
}