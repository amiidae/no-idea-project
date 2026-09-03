using Bnny.Scripts.Services;
using Bnny.Scripts.Services.Input;
using UnityEngine;

namespace Bnny.Scripts //                                                                               /*⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣠⣤⣦⣤⣄⡀⠀⠀⠀⠀⢀⣀⣀⣀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀*/
{ //                                                                                                    /*⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣰⠟⠙⠀⠀⠀⠈⢻⡆⠀⣴⠞⠋⠉⠉⠙⠳⣦⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀*/
    public class DebugPanelController : MonoBehaviour //                                                /*⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢸⡛⠂⠀⠀⠀⠀⠀⠈⣿⣾⠋⠀⠀⠀⠀⠀⠀⠈⣿⡄⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀*/
    { //                                                                                                /*⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣽⠁⠀⠀⠀⠀⠀⠀⠀⣽⢇⠀⠀⠀⠀⠀⠀⠀⠀⢸⡇⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀*/
        [SerializeField] //                                                                             /*⠀⠀⠀⠀⠀⠀⠀⠀⠀⢨⡟⠀⠀⠀⠀⠀⠀⠀⢸⡇⠀⠀⠀⠀⠀⠀⠀⠀⠀⣿⠇⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀*/
        private GameObject debugPanel; //                                                               /*⠀⠀⠀⠀⠀⠀⠀⠀⠀⠈⣿⠀⠀⠀⠀⠀⠀⠀⢸⡇⠀⠀⠀⠀⠀⠀⠀⠀⢠⡿⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀*/
        private const string panelName = "GraphyDebugPanel"; //                                         /*⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣿⡆⠀⠀⢀⣀⣀⡀⢸⣇⠀⠀⠀⠀⠀⠀⠀⢀⣾⠃⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀*/
        private GameObject activePanel; //                                                              /*⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣘⡟⠰⠛⠛⠉⠙⠉⠈⠃⠀⠀⠀⠀⠀⠀⢰⣾⡟⠚⢶⣄⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀*/
        bool isDebugPanelActive; //                                                                     /*⠀⠀⠀⠀⠀⠀⠀⠀⣤⡾⠋⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠈⡁⠀⢀⡬⢹⡇⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀*/
        private IInputService inputService; //                                                          /*⠀⠀⠀⠀⠀⠀⠀⣴⠟⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣷⠀⠚⢷⣼⡷⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀*/

        void Start() //                                                                                 /*⠀⠀⠀⠀⠀⠀⣼⠇⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢙⣷⠀⠀⠘⢿⣷⠀⠀⠀⠀⠀⠀⠀⠀⠀*/
        { //                                                                                            /*⠀⠀⠀⠀⠀⢸⡟⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢠⣇⠀⠀⠀⢹⣧⠀⠀⠀⠀⠀⠀⠀⠀*/
#if DEBUG   //                                                                                          /*⠀⠀⠀⠀⠀⣿⢣⣷⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢸⡏⣡⠀⠀⠀⠻⣧⠀⠀⠀⠀⠀⠀⠀*/
            activePanel = GameObject.Instantiate(debugPanel, gameObject.transform); //                  /*⠀⠀⠀⠀⠀⣿⡾⡿⠖⠀⠀⠀⠀⠀⠀⠀⠀⢀⣶⣿⣤⠀⠀⠀⠀⠀⠀⠀⣼⡇⠃⠀⠀⠀⠀⢹⣇⠀⠀⠀⠀⠀*/
            activePanel.name = panelName; //                                                            /*⠀⠀⠀⠀⠀⠹⣧⡀⠀⠀⠰⣦⣸⣶⠄⠀⠀⠸⡿⠿⠇⠀⠀⠀⠀⠀⠀⢢⡿⠅⠀⠀⠀⠀⠀ ⣿⠀⠀⠀⠀⠀*/
            isDebugPanelActive = PlayerPrefs.GetInt("DebugPanelActive", 1) == 1; //                     /*⠀⠀⠀⠀⠀⠀⠈⠻⣦⣒⠸⠛⠻⠖⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⣼⠟⠁⠀⠀⠀⠀⣄⠀⠀⣾⠀⠀⠀⠀⠀*/
            activePanel.SetActive(isDebugPanelActive); //                                               /*⠀⠀⠀⠀⠀⠀⠀⠀⠈⢙⣷⢶⣤⣀⣀⠀⠀⠀⠀⠀⠀⠀⣀⣤⡶⠟⠁⠀⠀⠀⠀⠀⣼⢏⣠⣾⠟⠀⠀⠀⠀⠀*/

            inputService = ServiceLocator.GetService<IInputService>(); //                                /*⠀⠀⠀⠀⠀⠀⠀⠀⢀⣾⠃⠀⠀⠉⠛⠛⠻⠶⠶⠶⠶⠞⠋⠁⠀⠀⠀⠀⠀⠀⣰⡾⠛⠛⠉⠀⠀⠀⠀⠀⠀⠀*/
            inputService.ToggleDebug += OnToggleDebug; //                                                /*⠀⠀⠀⠀⠀⠀⠀⠀⠘⣿⠀⠀⠀⠀⠀⢲⡇⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⡀⣠⡾⠏⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀*/
#endif   //                                                                                              /*⠀⠀⠀⠀⠀⠀⠀⠀⠀⠻⣧⡀⠀⠀⣡⣿⠛⠻⠶⣾⠀⠀⠀⠀⠀⠀⠈⢾⡟⠆⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀*/
            //                                                                                           /*⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠉⠛⠛⠛⠋⠁⠀⠀⠀⢿⣦⠀⠀⠀⠀⠀⣠⡾⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀*/
        } //                                                                                             /*⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠈⠻⣶⣤⣀⣦⣴⡟⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀*/

        private void OnToggleDebug()
        {
            if (activePanel.activeSelf == true)
            {
                isDebugPanelActive = false;
            }
            else
            {
                isDebugPanelActive = true;
            }
            activePanel.SetActive(isDebugPanelActive);
            PlayerPrefs.SetInt("DebugPanelActive", isDebugPanelActive ? 1 : 0);
        }

        void OnDestroy()
        {
            inputService.ToggleDebug -= OnToggleDebug;
        }
    }
}
