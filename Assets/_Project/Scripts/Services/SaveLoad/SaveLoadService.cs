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

    public SaveLoadService(ISerializer serializer)
    {
        this.serializer = serializer;
    }

    public void AddProgressUser(IProgressUser progressUser)
    {
        // Question: progressUser.GetType() == typeof(IProgressReader)
        if (progressUser is IProgressReader)
        {
            progressReaders.Add((IProgressReader)progressUser);
        }
        else
        {
            progressWriters.Add((IProgressWriter)progressUser);
        }
    }

    public void RemoveProgressUser(IProgressUser progressUser)
    {
        if (progressUser is IProgressReader)
        {
            progressReaders.Remove((IProgressReader)progressUser);
        }
        else
        {
            progressWriters.Remove((IProgressWriter)progressUser);
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
        throw new NotImplementedException();
    }
}
