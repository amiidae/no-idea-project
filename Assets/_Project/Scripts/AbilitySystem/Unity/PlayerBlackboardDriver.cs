using Bnny.Scripts.AbilitySystem.Core;
using Bnny.Scripts.Services;
using Bnny.Scripts.Services.Input;
using UnityEngine;

namespace Bnny.Scripts.AbilitySystem.Unity
{
    public class PlayerBlackboardDriver : MonoBehaviour
    {
        private IInputService inputService;
        private IAbilityUser abilityUser;

        void Start()
        {
            inputService = ServiceLocator.GetService<IInputService>();

            abilityUser = gameObject.GetComponent<IAbilityUser>();
        }

        void Update()
        {
            abilityUser.AbilityUserBlackboard.SetAxis2D(InputTypeId.Move, inputService.MoveAxis);

            abilityUser.AbilityUserBlackboard.SetState(
                InputTypeId.Run,
                inputService.IsRunInputReceived
            );

            abilityUser.AbilityUserBlackboard.SetState(
                InputTypeId.Jump,
                inputService.IsJumpInputReceived
            );
            abilityUser.AbilityUserBlackboard.SetState(
                InputTypeId.LongJump,
                inputService.IsJumpInputContinuous
            );
        }
    }
}
