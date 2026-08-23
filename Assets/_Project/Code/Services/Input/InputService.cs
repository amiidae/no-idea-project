using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Code.Services.Input
{
    public class InputService : IInputService, IInitializableService, GameInput.IGameplayActions
    {
        public event Action Save;
        
        public Vector2 MoveAxis
        {
            get
            {
                return _input.Player.Move.ReadValue<Vector2>();
            }
        }

        public bool IsJumpInputReceived
        {
            get
            {
                return _input.Player.Jump.WasPerformedThisFrame();
            }
        }

        public bool IsLongJumpInputReceived
        {
            get
            {
                return _input.Player.Jump.IsPressed();
            }
        }

        public bool IsRunInputReceived
        {
            get
            {
                return _input.Player.Sprint.IsPressed();
            }
        }

        private GameInput _input;

        public void Initialize()
        {
            _input = new GameInput();
            _input.Gameplay.SetCallbacks(this);

            _input.Gameplay.Enable();
            _input.Player.Enable();
        }

        public void OnSave(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                Save?.Invoke();
            }
        }
    }
}
