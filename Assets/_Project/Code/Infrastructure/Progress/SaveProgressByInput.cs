using UnityEngine;
using VContainer.Unity;

namespace Code.Services.Progress
{
    public class SaveProgressByInput : ISaveProgressStrategy, IInitializable
    {
        private readonly IInputService _inputService;
        private readonly ISaveLoadService _saveLoadService;

        public SaveProgressByInput(ISaveLoadService saveLoadService, IInputService inputService)
        {
            _saveLoadService = saveLoadService;
            _inputService = inputService;
        }

        public void Initialize()
        {
            _inputService.Save += OnSaveProgress;
        }

        private void OnSaveProgress()
        {
            _saveLoadService.SaveProgress();
        }
    }
}