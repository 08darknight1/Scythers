using UnityEngine;

public class PlaceholderDebugOption : DebugOption
{
    public override void SetupOption()
    {
        Debug.Log("Setado op��o de Debug: PLACEHOLDER!");
    }

    public override void ExecuteOption()
    {
        Debug.Log("Executado op��o de Debug: PLACEHOLDER!");
    }

    public override void TerminateOption()
    {
        Debug.Log("Destruido op��o de Debug: PLACEHOLDER!");
    }
}
