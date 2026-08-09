using UnityEngine;

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
        abilityUser.AbilityUserBlackboard.SetAxis2D((int)InputTypeId.Move, inputService.MoveAxis);

        abilityUser.AbilityUserBlackboard.SetState(
            (int)InputTypeId.Run,
            inputService.IsRunInputReceived
        );

        abilityUser.AbilityUserBlackboard.SetState(
            (int)InputTypeId.Jump,
            inputService.IsJumpInputReceived
        );
        abilityUser.AbilityUserBlackboard.SetState(
            (int)InputTypeId.LongJump,
            inputService.IsJumpInputContinuous
        );
    }
}
