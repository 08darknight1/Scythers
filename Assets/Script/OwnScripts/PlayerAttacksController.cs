using Rewired;
using UnityEngine;

public class PlayerAttacksController : MonoBehaviour
{
    private PlayerData _playerData;

    private Player _playerRewired;

    private GameObject _playerAim;

    private Camera _mainCamera;

    private PlayerAimLimiter _playerAimLimiter;

    private SpriteRenderer _spriteRenderer;
    
    
    void Start()
    {
        _playerData = gameObject.GetComponent<PlayerData>();
        _playerRewired = _playerData.ReturnPlayer();
        _playerAim = gameObject.transform.GetChild(0).gameObject;
        _mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        _spriteRenderer = gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
        _playerAimLimiter = gameObject.transform.GetChild(1).gameObject.GetComponent<PlayerAimLimiter>();
    }

    // Update is called once per frame
    void Update()
    {
        var fRadius = 1f;
        var mousePos = ReInput.controllers.Mouse.screenPosition;
        var targetPos = _mainCamera.WorldToScreenPoint(gameObject.transform.position);
        var mousePosV3 = new Vector3(mousePos.x, mousePos.y, 0);

        var mouseMinusTargetPos = mousePosV3 - targetPos;
 
        float angle = Mathf.Atan2 (mouseMinusTargetPos.y, mouseMinusTargetPos.x) * Mathf.Rad2Deg;
        mouseMinusTargetPos = Quaternion.AngleAxis (angle, Vector3.forward) * (Vector3.right * fRadius);

        _playerAim.transform.position = gameObject.transform.position + mouseMinusTargetPos;
        
        Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - _playerAim.transform.position;
        diff.Normalize();
 
        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        _playerAim.transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);

        var flipSpriteY = false;

        if (_playerAimLimiter.ReturnMouseIsOnPlayer())
        {
            if (mouseMinusTargetPos.x < 1.01f && mouseMinusTargetPos.x > -1.01f)
            {
                if (mouseMinusTargetPos.y < 1.01f && mouseMinusTargetPos.y > -1.01f)
                {
                    flipSpriteY = true;
                }
            }
        }

        _spriteRenderer.flipY = flipSpriteY;
    }
}
