using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputSystemService : IInputService
{
    InputActionAsset InputActions;

    private InputAction moveAction;
    private InputAction runAction;
    private InputAction jumpAction;

    public InputSystemService()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        runAction = InputSystem.actions.FindAction("Sprint");
        jumpAction = InputSystem.actions.FindAction("Jump");

        InputActions = Resources.Load<InputActionAsset>("InputActions/InputSystem_Actions");
        InputActions.FindActionMap("Player").Enable();
    }

    ~InputSystemService()
    {
        InputActions.FindActionMap("Player").Disable();
    }

    public Vector2 MoveAxis
    {
        get
        {
            Vector2 inputVector = moveAction.ReadValue<Vector2>();
            // Debug.Log(inputVector.x);

            return inputVector;
        }
    }

    public bool IsMoveInputReceived
    {
        get { return Math.Abs(MoveAxis.x) > 0; }
    }
    public bool IsRunInputReceived
    {
        get
        {
            bool isRunKeyHeld = runAction.IsPressed();
            return isRunKeyHeld;
        }
    }
    public bool IsJumpInputReceived
    {
        get
        {
            bool isJumpKeyDown = jumpAction.WasPressedThisFrame();
            return isJumpKeyDown;
        }
    }

    public bool IsJumpInputContinuous
    {
        get
        {
            bool isJumpKeyHeld = jumpAction.IsPressed();
            return isJumpKeyHeld;
        }
    }
}
