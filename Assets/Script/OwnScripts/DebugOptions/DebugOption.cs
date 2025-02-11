using UnityEngine;

public class DebugOption : MonoBehaviour
{
    private string _debugOptionName;

    private int _debugOptionID;

    private bool _optionActive;

    public virtual void SetupOption()
    {
        //Colocar aqui todos os elementos necessários para iniciar a opção de Debug!
    }
    
    public virtual void ExecuteOption()
    {
        //Colocar aqui todos os elementos necessários para a execução da opção de Debug!
    }

    public virtual void TerminateOption()
    {
        //Colocar aqui todos os elementos necessários para a finalizar a execução da opção de Debug!
    }

    public void SetOptionActive(bool valueToSet)
    {
        _optionActive = valueToSet;
        //Debug.Log("Setting option active right now!");
    }

    public bool ReturnOptionActive()
    {
        return _optionActive;
    }

    public string ReturnOptionName()
    {
        return _debugOptionName;
    }

    public int ReturnOptionID()
    {
        return _debugOptionID;
    }

    public void SetNameAndId()
    {
        _debugOptionName = gameObject.GetType().ToString();
        _debugOptionID = Random.Range(0, 100000000);
    }
}
