using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DebugLog : MonoBehaviour
{
    public string output = "";
    public string stack = "";
    public LogType level;

    private Transform _CenterCamera;
    private int counter;
    private string[] messages;

    //FUNCTIONS===================================
    void Awake() {
        _CenterCamera = transform.parent;
        transform.rotation = Quaternion.LookRotation(transform.position - _CenterCamera.position);
    }

    void OnEnable() { Application.logMessageReceived += HandleLog; }
    void OnDisable() { Application.logMessageReceived -= HandleLog; }

    void HandleLog(string logString, string stackTrace, LogType type) {
        output = logString;
        stack = stackTrace;
        level = type;

        TMP_Text targ = null;
        TMP_Text quell = null;

        for (int x = 1; x < 19; x++) {

            targ = transform.GetChild(x - 1).GetComponent<TMP_Text>();
            quell = transform.GetChild(x).GetComponent<TMP_Text>();

            targ.text = quell.text;
            targ.color = quell.color;
        }

        quell.text = counter.ToString() + "|" + output;

        switch (level) {
            case LogType.Assert:
            case LogType.Error:
            case LogType.Exception:
                quell.color = Color.red;
                break;
            case LogType.Warning:
                quell.color = Color.yellow;
                break;
            case LogType.Log:
            default:
                quell.color = Color.white;
                break;
        }

        counter++;
    }
}
