namespace Code.Services.Progress
{
    public class SaveProgressByInput : ISaveProgressStrategy
    {
        private IInputService _inputService;
        private SaveLoadService _saveLoadService;

        public SaveProgressByInput(SaveLoadService saveLoadService, IInputService inputService)
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