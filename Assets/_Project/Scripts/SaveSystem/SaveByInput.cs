using System;
using Bnny.Scripts.Services.Input;
using Bnny.Scripts.Services.SaveLoad;
using UnityEngine;
using VContainer.Unity;

namespace Bnny.Scripts.SaveSystem
{
    public class SaveByInput : IInitializable, IDisposable
    {
        private readonly IInputService inputService;
        private readonly ISaveLoadService saveLoadService;

        public SaveByInput(IInputService inputService, ISaveLoadService saveLoadService)
        {
            this.inputService = inputService;
            this.saveLoadService = saveLoadService;
        }

        public void Initialize()
        {
            inputService.Save += OnSave;
        }

        public void Dispose()
        {
            inputService.Save -= OnSave;
        }

        private async void OnSave()
        {
            await saveLoadService.SaveProgress();
            // Debug.Log($"Saved at {saveLoadService.SaveFile}");

            // Question:
            // should properties of SaveLoadService be in the interface ISaveLoadService?
        }
    }
}
