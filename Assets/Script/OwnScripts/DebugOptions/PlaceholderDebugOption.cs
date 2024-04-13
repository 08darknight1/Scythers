using UnityEngine;

public class PlaceholderDebugOption : DebugOption
{
    public override void SetupOption()
    {
        Debug.Log("Setado opção de Debug: PLACEHOLDER!");
    }

    public override void ExecuteOption()
    {
        Debug.Log("Executado opção de Debug: PLACEHOLDER!");
    }

    public override void TerminateOption()
    {
        Debug.Log("Destruido opção de Debug: PLACEHOLDER!");
    }
}
