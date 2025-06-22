using UnityEngine;
using System.Collections.Generic;

public class GameDebugger : MonoBehaviour
{    
    public bool IsDebugModeOn;
    
    public List<DebugOption> AllDebugOptions = new List<DebugOption>();

    public bool[] ActiveOptions;

    private bool debugOn;

    private int debugOptionIndex;

    void Start()
    {
        ActiveOptions = new bool[AllDebugOptions.Count];

        for(int x = 0; x < AllDebugOptions.Count; x++)
        {
            AllDebugOptions[x].SetNameAndId();
            AllDebugOptions[x].SetOptionActive(false);
            ActiveOptions[x] = false;
        }
    }
    
    void Update()
    { 
        CheckForButtonPresses();
        
        UpdateActiveOptions();

        IsDebugModeOn = debugOn;

        if (debugOn)
        {
            ExecuteAllActiveOptions();
        }
    }

    private void CheckForButtonPresses()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {   
            debugOn = !debugOn;

            if (!debugOn)
            {
                TerminateAllActiveOptions();
            }
            else
            {
                var _DebugUI = new GameObject().AddComponent<DebuggerUI>();
                _DebugUI.gameObject.name = "DebugModeIsOn";
            }
        }
        else if(Input.GetKeyDown(KeyCode.F2))
        {
            if (debugOn)
            {
                AddToDebugOptionIndex();
            }
        }
        else if (Input.GetKeyUp(KeyCode.F3))
        {
            if (debugOn)
            {
                SetupActiveOptionByIndex();
            }
        }
    }

    private void AddToDebugOptionIndex()
    {
        if (AllDebugOptions.Count > 1)
        {
            var additionToIndex = debugOptionIndex + 1;

            if (additionToIndex > AllDebugOptions.Count - 1)
            {
                debugOptionIndex = 0;
            }
            else
            {
                debugOptionIndex = additionToIndex;
            }
        }

        //Debug.Log("Current Debug Option Index: " + debugOptionIndex);
    }

    private void UpdateActiveOptions()
    {
        for (int x = 0; x < ActiveOptions.Length; x++)
        {
            ActiveOptions[x] = AllDebugOptions[x].ReturnOptionActive();
            //Debug.Log("Debug option[" + x + "] is active: " + AllDebugOptions[x].ReturnOptionActive());
        }
    }

    private void ExecuteAllActiveOptions()
    {
        for (int x = 0; x < AllDebugOptions.Count; x++)
        {
            //Debug.Log("Is Debug Option[" + x + "] on: " + AllDebugOptions[x].ReturnOptionActive());

            if (AllDebugOptions[x].ReturnOptionActive())
            {
                AllDebugOptions[x].ExecuteOption();
            }
        }
    }
    
    private void SetupActiveOptionByIndex()
    {
        if (!AllDebugOptions[debugOptionIndex].ReturnOptionActive())
        {
            AllDebugOptions[debugOptionIndex].SetupOption();
            AllDebugOptions[debugOptionIndex].SetOptionActive(true);
        }
        else
        {
            AllDebugOptions[debugOptionIndex].TerminateOption();
            AllDebugOptions[debugOptionIndex].SetOptionActive(false);
        }
    }
    
    private void TerminateAllActiveOptions()
    {
        for (int x = 0; x < AllDebugOptions.Count; x++)
        {
            if (AllDebugOptions[x].ReturnOptionActive())
            {
                AllDebugOptions[x].TerminateOption();
                AllDebugOptions[x].SetOptionActive(false);
            }
        }
    }

    public bool ReturnIsDebugModeOn()
    {
        return debugOn;
    }

    public int ReturnDebugOptionIndex()
    {
        return debugOptionIndex;
    }

    public bool ReturnDebugOptionActive()
    {
        return AllDebugOptions[debugOptionIndex].ReturnOptionActive();
    }

    public string ReturnCurrentDebugOptionName()
    {
        return AllDebugOptions[debugOptionIndex].ReturnOptionName();
    }
}
