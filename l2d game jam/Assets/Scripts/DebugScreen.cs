using UnityEngine;

public class DebugScreen : MonoBehaviour
{
    public enum DebugMode
    {
        On,
        Off
    }

    public DebugMode debugMode = DebugMode.Off;
    public GameObject debugScreen;
    private CanvasGroup canvasGroup;

    void Start()
    {
        canvasGroup = debugScreen.GetComponent<CanvasGroup>();
        SetDebugMode(debugMode);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F3))
        {
            debugMode = (debugMode == DebugMode.Off) ? DebugMode.On : DebugMode.Off;
            SetDebugMode(debugMode);
        }
    }

    void SetDebugMode(DebugMode mode)
    {
        bool visible = (mode == DebugMode.On);
        canvasGroup.alpha = visible ? 1f : 0f;
        canvasGroup.interactable = visible;
        canvasGroup.blocksRaycasts = visible;
    }
}
