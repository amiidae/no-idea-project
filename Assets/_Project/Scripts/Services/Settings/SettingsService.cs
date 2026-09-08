using UnityEngine;

namespace Bnny.Scripts.Services.Settings
{
    public class SettingsService : ISettingsService
    {
        public bool IsDebugPlayerActiveSetting
        {
            get { return PlayerPrefs.GetInt("DebugPanelActive", 1) == 1; }
            set { PlayerPrefs.SetInt("DebugPanelActive", value ? 1 : 0); }
        }
    }
}
