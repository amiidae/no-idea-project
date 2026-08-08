using UnityEngine;

public class DebugPanelController : MonoBehaviour
{
    [SerializeField]
    private GameObject debugPanel;
    private const string panelName = "GraphyDebugPanel";

    void Start()
    {
#if DEBUG
        GameObject activePanel = GameObject.Instantiate(debugPanel, gameObject.transform);
        activePanel.name = panelName;
#endif
    }
}
