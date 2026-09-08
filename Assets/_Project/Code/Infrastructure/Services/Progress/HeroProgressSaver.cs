using System;
using Extensions;
using UnityEngine;

namespace Code.Services.Progress
{
    public class HeroProgressSaver : MonoBehaviour, IProgressReader
    {
        [SerializeField] private HeroController _heroController;

        private void Start()
        {
            
        }

        private void OnDestroy()
        {
            
        }

        public void ReadProgress(ProgressData progressData)
        {
            _heroController.Warp(progressData.HeroProgressData.Position.ToUnityVector());
            _heroController.FaceDirection(progressData.HeroProgressData.FacingX);
        }

        public void WriteProgress(ProgressData progressData)
        {
            progressData.HeroProgressData.Position = _heroController.Rigidbody.position.ToVector2Data();
            progressData.HeroProgressData.FacingX = _heroController.FacingX;
        }
    }
}