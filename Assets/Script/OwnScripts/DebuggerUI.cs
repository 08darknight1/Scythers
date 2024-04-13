using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DebuggerUI : MonoBehaviour
{
    private Canvas _debuggerCanvas;

    private TextMeshProUGUI _debugText;

    private GameDebugger _gameDebugger;

    private void Start()
    {
        _debuggerCanvas = new GameObject().AddComponent<Canvas>();

        _debuggerCanvas.name = "DebugUI";

        _debuggerCanvas.renderMode = 0;
        _debuggerCanvas.targetDisplay = 0;
        _debuggerCanvas.additionalShaderChannels = 0;

        _debuggerCanvas.gameObject.AddComponent<CanvasScaler>();

        _debuggerCanvas.GetComponent<CanvasScaler>().uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        _debuggerCanvas.GetComponent<CanvasScaler>().matchWidthOrHeight = 0.5f;

        _debugText = new GameObject().AddComponent<TextMeshProUGUI>();
        _debugText.transform.SetParent(_debuggerCanvas.transform);
        _debugText.name = "DebugText";


        _debugText.transform.localPosition = new Vector3(-350, -240, 0);
        _debugText.fontSize = 25;
        _debugText.horizontalAlignment = HorizontalAlignmentOptions.Left;
        _debugText.verticalAlignment = VerticalAlignmentOptions.Middle;
        _debugText.enableWordWrapping = false;
    
        _debuggerCanvas.transform.SetParent(GameObject.Find("DebugModeIsOn").transform);
    }

    private void Update()
    {
        if (_gameDebugger != null)
        {
            if (_gameDebugger.ReturnIsDebugModeOn())
            {
                _debugText.text = "DEBUG MODE |" +
                " [" + _gameDebugger.ReturnDebugOptionIndex() + "] - " +
                _gameDebugger.ReturnCurrentDebugOptionName() + "(" + _gameDebugger.ReturnCurrentDebugOptionId() + ")" +
                " - " + _gameDebugger.ReturnDebugOptionActive();
            }
            else
            {
                Destroy(gameObject);
            }
        }
        else
        {
            _gameDebugger = GameObject.Find("GameDebugger").GetComponent<GameDebugger>();
        }
    }
}

