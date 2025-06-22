using Rewired;
using UnityEngine;

public class MouseDataDebugOption  : DebugOption
{
    public Sprite spriteForMousePos;
    
    public Material DefaultDebugMaterial;

    private GameObject _debugImage, _aimLine;

    public override void SetupOption()
    {
        SetupMouseImage();
        SetupMouseLaserAim();
    }
    
    public override void ExecuteOption()
    {
        var mousePos = ReInput.controllers.Mouse.screenPosition;
        var mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        var mouseWorldPos = mainCamera.ScreenToWorldPoint(mousePos);
        mouseWorldPos = new Vector3(mouseWorldPos.x, mouseWorldPos.y, 0);
        _debugImage.transform.position = mouseWorldPos;
        
        _aimLine.GetComponent<LineRenderer>().SetPosition(0, _aimLine.transform.parent.position);
        _aimLine.GetComponent<LineRenderer>().SetPosition(1, mouseWorldPos);
    }
    
    public override void TerminateOption()
    {
        Destroy(_debugImage);
        Destroy(_aimLine);
    }

    private void SetupMouseImage()
    {
        _debugImage = new GameObject();
        _debugImage.name = "DebugMousePosIcon";
        _debugImage.AddComponent<SpriteRenderer>();
        _debugImage.GetComponent<SpriteRenderer>().sprite = spriteForMousePos;
        _debugImage.GetComponent<SpriteRenderer>().color = Color.yellow;
        _debugImage.GetComponent<SpriteRenderer>().renderingLayerMask = 100;
        _debugImage.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
    }

    private void SetupMouseLaserAim()
    {
        _aimLine = new GameObject();
        _aimLine.name = "PlayerAimDebugLine";
        var player = GameObject.FindGameObjectWithTag("Player");
        _aimLine.transform.parent = player.transform;
        _aimLine.AddComponent<LineRenderer>();
        var newMaterial = DefaultDebugMaterial;   
        newMaterial.color = Color.red;
        _aimLine.GetComponent<LineRenderer>().material = newMaterial;
        _aimLine.GetComponent<LineRenderer>().alignment = LineAlignment.TransformZ;
        _aimLine.GetComponent<LineRenderer>().widthMultiplier = 0.10f;
    }
}

