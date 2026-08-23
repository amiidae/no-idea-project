using System.IO;
using Code.Services.Progress;
using UnityEditor;
using UnityEngine;

namespace Code.Editor
{
    public static class SaveTools
    {
        [MenuItem("Tools/Saves/Clear Saves")]
        public static void ClearSaves()
        {
            if (Directory.Exists(SaveLoadService.SavesFolder))
            {
                Directory.Delete(SaveLoadService.SavesFolder, true);
            }
            
            PlayerPrefs.DeleteAll();
        }

        [MenuItem("Tools/Saves/Open Saves Location")]
        public static void OpenSavesLocation()
        {
            Application.OpenURL(SaveLoadService.SavesFolder);
        }
    }
}