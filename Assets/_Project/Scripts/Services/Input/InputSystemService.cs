using System;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer.Unity;

namespace Bnny.Scripts.Services.Input
{
    public class InputSystemService
        : IInputService,
            IInitializableService,
            IInitializable,
            InputSystemActions.IGameplayActions
    {
        public event Action Save;
        public event Action ToggleDebug;
        private InputSystemActions inputActions;

        private InputAction moveAction;
        private InputAction runAction;
        private InputAction jumpAction;

        public InputSystemService() { }

        ~InputSystemService()
        {
            inputActions.Player.Disable();
            inputActions.Gameplay.Disable();
        }

        public Vector2 MoveAxis
        {
            get
            {
                Vector2 inputVector = moveAction.ReadValue<Vector2>();

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

        public void Initialize()
        {
            inputActions = new InputSystemActions();

            moveAction = inputActions.Player.Move;
            runAction = inputActions.Player.Sprint;
            jumpAction = inputActions.Player.Jump;

            inputActions.Gameplay.SetCallbacks(this);

            inputActions.Player.Enable();
            inputActions.Gameplay.Enable();
        }

        public void OnSave(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                Save?.Invoke();
            }
        }

        public void OnToggleDebug(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                ToggleDebug?.Invoke();
            }
        }
    }
}
