using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using VContainer.Unity;

namespace Code.Services.Progress
{
    public class SaveLoadService : ISaveLoadService
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

        private readonly HashSet<IProgressWatcher> _progressWatchers = new();
        
        private ProgressData _progressData;
        
        private readonly ISerializer _serializer;

        public SaveLoadService(ISerializer serializer)
        {
            _serializer = serializer;
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
            if (!TryGetLoadedProgressData(out _))
            {
                _progressData = CreateNewProgress();
            }
            
            foreach (IProgressWriter progressWriter in _progressWatchers.OfType<IProgressWriter>())
            {
                progressWriter.WriteProgress(_progressData);
            }
            
            string json = _serializer.Serialize(_progressData);
            await File.WriteAllTextAsync(TempSaveFilePath, json);
            
            File.Delete(SaveFilePath);
            File.Move(TempSaveFilePath, SaveFilePath);
        }

        public async Task LoadProgress()
        {
            if (File.Exists(SaveFilePath))
            {
                try
                {
                    string json = await File.ReadAllTextAsync(SaveFilePath);
                    _progressData = _serializer.Deserialize<ProgressData>(json);
                }
                catch (Exception exception)
                {
                    Debug.LogError($"Failed to load progress: {exception.Message}");
                    _progressData = CreateNewProgress();
                }
            }
            else
            {
                _progressData = CreateNewProgress();
            }
            
            foreach (IProgressReader progressReader in _progressWatchers.OfType<IProgressReader>())
            {
                progressReader.ReadProgress(_progressData);
            }
        }

        public bool TryGetLoadedProgressData(out ProgressData progressData)
        {
            progressData = _progressData;
            return progressData != null;
        }

        private ProgressData CreateNewProgress()
        {
            return new ProgressData();
        }
    }
}