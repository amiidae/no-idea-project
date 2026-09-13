using Bnny.Scripts.Services.Input;
using Bnny.Scripts.Services.Settings;
using UnityEngine;
using VContainer;

/*⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣠⣤⣦⣤⣄⡀⠀⠀⠀⠀⢀⣀⣀⣀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀*/
/*⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣰⠟⠙⠀⠀⠀⠈⢻⡆⠀⣴⠞⠋⠉⠉⠙⠳⣦⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀*/
/*⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢸⡛⠂⠀⠀⠀⠀⠀⠈⣿⣾⠋⠀⠀⠀⠀⠀⠀⠈⣿⡄⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀*/
/*⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣽⠁⠀⠀⠀⠀⠀⠀⠀⣽⢇⠀⠀⠀⠀⠀⠀⠀⠀⢸⡇⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀*/
/*⠀⠀⠀⠀⠀⠀⠀⠀⠀⢨⡟⠀⠀⠀⠀⠀⠀⠀⢸⡇⠀⠀⠀⠀⠀⠀⠀⠀⠀⣿⠇⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀*/
/*⠀⠀⠀⠀⠀⠀⠀⠀⠀⠈⣿⠀⠀⠀⠀⠀⠀⠀⢸⡇⠀⠀⠀⠀⠀⠀⠀⠀⢠⡿⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀*/
/*⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣿⡆⠀⠀⢀⣀⣀⡀⢸⣇⠀⠀⠀⠀⠀⠀⠀⢀⣾⠃⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀*/
/*⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣘⡟⠰⠛⠛⠉⠙⠉⠈⠃⠀⠀⠀⠀⠀⠀⢰⣾⡟⠚⢶⣄⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀*/
/*⠀⠀⠀⠀⠀⠀⠀⠀⣤⡾⠋⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠈⡁⠀⢀⡬⢹⡇⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀*/
/*⠀⠀⠀⠀⠀⠀⠀⣴⠟⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣷⠀⠚⢷⣼⡷⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀*/
/*⠀⠀⠀⠀⠀⠀⣼⠇⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢙⣷⠀⠀⠘⢿⣷⠀⠀⠀⠀⠀⠀⠀⠀⠀*/
/*⠀⠀⠀⠀⠀⢸⡟⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢠⣇⠀⠀⠀⢹⣧⠀⠀⠀⠀⠀⠀⠀⠀*/
/*⠀⠀⠀⠀⠀⣿⢣⣷⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢸⡏⣡⠀⠀⠀⠻⣧⠀⠀⠀⠀⠀⠀⠀*/
/*⠀⠀⠀⠀⠀⣿⡾⡿⠖⠀⠀⠀⠀⠀⠀⠀⠀⢀⣶⣿⣤⠀⠀⠀⠀⠀⠀⠀⣼⡇⠃⠀⠀⠀⠀⢹⣇⠀⠀⠀⠀⠀*/
/*⠀⠀⠀⠀⠀⠹⣧⡀⠀⠀⠰⣦⣸⣶⠄⠀⠀⠸⡿⠿⠇⠀⠀⠀⠀⠀⠀⢢⡿⠅⠀⠀⠀⠀⠀ ⣿⠀⠀⠀⠀⠀*/
/*⠀⠀⠀⠀⠀⠀⠈⠻⣦⣒⠸⠛⠻⠖⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⣼⠟⠁⠀⠀⠀⠀⣄⠀⠀⣾⠀⠀⠀⠀⠀*/
/*⠀⠀⠀⠀⠀⠀⠀⠀⠈⢙⣷⢶⣤⣀⣀⠀⠀⠀⠀⠀⠀⠀⣀⣤⡶⠟⠁⠀⠀⠀⠀⠀⣼⢏⣠⣾⠟⠀⠀⠀⠀⠀*/
/*⠀⠀⠀⠀⠀⠀⠀⠀⢀⣾⠃⠀⠀⠉⠛⠛⠻⠶⠶⠶⠶⠞⠋⠁⠀⠀⠀⠀⠀⠀⣰⡾⠛⠛⠉⠀⠀⠀⠀⠀⠀⠀*/
/*⠀⠀⠀⠀⠀⠀⠀⠀⠘⣿⠀⠀⠀⠀⠀⢲⡇⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⡀⣠⡾⠏⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀*/
/*⠀⠀⠀⠀⠀⠀⠀⠀⠀⠻⣧⡀⠀⠀⣡⣿⠛⠻⠶⣾⠀⠀⠀⠀⠀⠀⠈⢾⡟⠆⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀*/
/*⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠉⠛⠛⠛⠋⠁⠀⠀⠀⢿⣦⠀⠀⠀⠀⠀⣠⡾⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀*/
/*⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠈⠻⣶⣤⣀⣦⣴⡟⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀*/

namespace Bnny.Scripts
{
    public class DebugPanelController : MonoBehaviour
    {
        [SerializeField]
        private GameObject debugPanelPrefab;
        private const string panelName = "GraphyDebugPanel";
        private GameObject activePanel;

        private IInputService inputService;
        private ISettingsService settingsService;

        [Inject]
        private void Construct(IInputService inputService, ISettingsService settingsService)
        {
            this.inputService = inputService;
            this.settingsService = settingsService;
        }

        void Start()
        {
#if DEBUG
            activePanel = GameObject.Instantiate(debugPanelPrefab, gameObject.transform);
            activePanel.name = panelName;

            activePanel.SetActive(settingsService.IsDebugPlayerActiveSetting);

            inputService.ToggleDebug += OnToggleDebug;
#endif
        }

        private void OnToggleDebug()
        {
            if (activePanel.activeSelf == true)
            {
                settingsService.IsDebugPlayerActiveSetting = false;
            }
            else
            {
                settingsService.IsDebugPlayerActiveSetting = true;
            }
            activePanel.SetActive(settingsService.IsDebugPlayerActiveSetting);
        }

        void OnDestroy()
        {
            inputService.ToggleDebug -= OnToggleDebug;
        }
    }
}
