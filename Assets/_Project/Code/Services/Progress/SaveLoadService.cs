using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Code.Services.Progress
{
    public class SaveLoadService : ISaveLoadService, IInitializableService
    {
        public static string SavesFolder
        {
            get
            {
                return Application.persistentDataPath;
            }
        }
        
        public static string SaveFilePath
        {
            get
            {
                return Path.Combine(SavesFolder, "savedata.save");
            }
        }
        
        public static string TempSaveFilePath
        {
            get
            {
                return Path.Combine(SavesFolder, "savedata.tmp");
            }
        }

        
        public ProgressData ProgressData { get; set; } = new();

        private readonly HashSet<IProgressWatcher> _progressWatchers = new();
        
        private readonly List<ISaveProgressStrategy> _saveProgressStrategies = new();
        
        private readonly ISerializer _serializer;

        public SaveLoadService(ISerializer serializer)
        {
            _serializer = serializer;
        }
        
        public void Initialize()
        {
            _saveProgressStrategies.Add(new SaveProgressByInput(this, ServiceLocator.GetService<IInputService>()));
        }

        public void AddProgressWatcher(IProgressWatcher progressWatcher)
        {
            _progressWatchers.Add(progressWatcher);
        }

        public void RemoveProgressWatcher(IProgressWatcher progressWatcher)
        {
            _progressWatchers.Remove(progressWatcher);
        }

        public async Task SaveProgress()
        {
            ProgressData.NewGame = false;
            
            foreach (IProgressWriter progressWriter in _progressWatchers.OfType<IProgressWriter>())
            {
                progressWriter.WriteProgress(ProgressData);
            }

            string json = _serializer.Serialize(ProgressData);

            await File.WriteAllTextAsync(TempSaveFilePath, json);
            
            if(File.Exists(SaveFilePath))
                File.Delete(SaveFilePath);
           
            File.Move(TempSaveFilePath, SaveFilePath);
        }

        public async Task LoadPrgoress()
        {
            if (!File.Exists(SaveFilePath))
            {
                ProgressData = new ProgressData();
            }


            try
            {
                string json = await File.ReadAllTextAsync(SaveFilePath);
                ProgressData = _serializer.Deserialize<ProgressData>(json) ?? new ProgressData();
            }
            catch (Exception exception)
            {
                Debug.LogException(exception);
            }
            
            foreach (IProgressReader progressReader in _progressWatchers.OfType<IProgressReader>())
            {
                progressReader.ReadProgress(ProgressData);
            }
        }
    }
}