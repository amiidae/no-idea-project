using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;

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

    public ProgressData ProgressData { get; private set; } = new ProgressData();

    private HashSet<IProgressReader> progressReaders = new HashSet<IProgressReader>();
    private HashSet<IProgressWriter> progressWriters = new HashSet<IProgressWriter>();

    private ISerializer serializer;
    private IInputService inputService;

    public SaveLoadService(ISerializer serializer, IInputService inputService)
    {
        this.serializer = serializer;
        this.inputService = inputService;

        inputService.Save += OnSave;
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
        foreach (IProgressWriter progressWriter in progressWriters)
        {
            progressWriter.SaveProgress(ProgressData);
        }

        string progressJson = serializer.Serialize<ProgressData>(ProgressData);

        await File.WriteAllTextAsync(TempSaveFile, progressJson);

        File.Delete(SaveFile);
        File.Move(TempSaveFile, SaveFile);

        Debug.Log("progress saved");
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
                ProgressData = serializer.Deserialize<ProgressData>(json);
            }
            catch (System.Exception e)
            {
                Debug.LogError(
                    $"Exception while Loading Progress: {e.Message}\nHave a good day <3"
                );
            }
        }
        else
        {
            ProgressData = new ProgressData();
        }

        foreach (IProgressReader progressReader in progressReaders)
        {
            progressReader.LoadProgress(ProgressData);
        }
    }

    private async void OnSave()
    {
        await SaveProgress();
        Debug.Log($"Saved at {SaveFile}");
    }
}
