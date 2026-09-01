using System;
using Bnny.Scripts.Data;
using Bnny.Scripts.Services;
using Bnny.Scripts.Services.SaveLoad;
using UnityEngine;

namespace Bnny.Scripts.SaveSystem
// Question:
// hidden dependency by passing information in the event?
{
    public class HeroProgressManager : MonoBehaviour, IProgressReader, IProgressWriter
    {
        // public event Action ProgressSaved;
        public event Action<Vector3> ProgressLoaded;

        private ISaveLoadService saveLoadService;

        // the line is drawn on the handling of ProgressData class

        void Start()
        {
            saveLoadService = ServiceLocator.GetService<ISaveLoadService>();

            saveLoadService.AddProgressUser(this);
        }

        public void SaveProgress(ProgressData progressData)
        {
            progressData.HeroProgressData.Position = gameObject.transform.position.ToVector3Data();

            // ProgressSaved.Invoke();
        }

        public void LoadProgress(ProgressData progressData)
        {
            Vector3 coordinates = progressData.HeroProgressData.Position.ToUnityVector3();

            ProgressLoaded.Invoke(coordinates);
        }

        void OnDestroy()
        {
            saveLoadService.RemoveProgressUser(this);
        }
    }
}
