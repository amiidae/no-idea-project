using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Bnny.Scripts.Data;
using Bnny.Scripts.SaveSystem;
using Bnny.Scripts.Services.Input;
using Bnny.Scripts.Services.Serializer;
using UnityEngine;

namespace Bnny.Scripts.Services.SaveLoad
{
    public class SaveLoadService : ISaveLoadService
    {
        public static string SaveDirectory
        {
            get { return Application.persistentDataPath; }
        }

        public static string TempSaveFile
        {
            get { return Path.Combine(SaveDirectory, "savefile.tmp"); }
        }

        public static string SaveFile
        {
            get { return Path.Combine(SaveDirectory, "savefile.save"); }
        }

        private HashSet<IProgressReader> progressReaders = new HashSet<IProgressReader>();
        private HashSet<IProgressWriter> progressWriters = new HashSet<IProgressWriter>();

        private ISerializer serializer;
        private IInputService inputService;

        private ProgressData progressData;

        public SaveLoadService(ISerializer serializer, IInputService inputService)
        {
            this.serializer = serializer;
            this.inputService = inputService;

            inputService.Save += OnSave;
        }

        public bool TryGetProgressData(out ProgressData progressData)
        {
            progressData = this.progressData;

            if (progressData == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void AddProgressUser(IProgressUser progressUser)
        {
            // Question: progressUser.GetType() == typeof(IProgressReader)
            if (progressUser is IProgressReader)
            {
                progressReaders.Add((IProgressReader)progressUser);
            }
            if (progressUser is IProgressWriter)
            {
                progressWriters.Add((IProgressWriter)progressUser);
            }
        }

        public void RemoveProgressUser(IProgressUser progressUser)
        {
            if (progressUser is IProgressReader reader)
            {
                progressReaders.Remove(reader);
            }
            if (progressUser is IProgressWriter writer)
            {
                progressWriters.Remove(writer);
            }
        }

        public async Task SaveProgress()
        {
            if (TryGetProgressData(out _) == false)
            {
                CreateNewProgress();
            }

            foreach (IProgressWriter progressWriter in progressWriters)
            {
                progressWriter.SaveProgress(progressData);
            }

            string json = serializer.Serialize<ProgressData>(progressData, true);

            await File.WriteAllTextAsync(TempSaveFile, json);

            File.Delete(SaveFile);
            File.Move(TempSaveFile, SaveFile);
        }

        public async Task LoadProgress()
        {
            if (File.Exists(SaveFile))
            {
                // Question:
                // when to try catch?
                try
                {
                    string json = await File.ReadAllTextAsync(SaveFile); // operation performed by side worker; working with files
                    progressData = serializer.Deserialize<ProgressData>(json);
                }
                catch (System.Exception e)
                {
                    Debug.LogError(
                        $"Exception while Loading Progress: {e.Message}\nHave a good day <3"
                    );
                    CreateNewProgress();
                }
            }
            else
            {
                CreateNewProgress();
            }

            foreach (IProgressReader progressReader in progressReaders)
            {
                progressReader.LoadProgress(progressData);
            }
        }

        private void CreateNewProgress()
        {
            progressData = new ProgressData();
        }

        private async void OnSave()
        {
            await SaveProgress();
            Debug.Log($"Saved at {SaveFile}");
        }
    }
}
