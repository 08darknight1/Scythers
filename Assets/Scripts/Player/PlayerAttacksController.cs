using Rewired;
using System.Threading;
using UnityEngine;

public class PlayerAttacksController : MonoBehaviour
{
    private PlayerData _playerData;

    private Player _playerRewired;

    private GameObject _playerAim;

    private Camera _mainCamera;

    private PlayerAimLimiter _playerAimLimiter;

    private SpriteRenderer _spriteRenderer;

    public GameObject ScythePrefab;

    private GameObject _scythe;

    private Rigidbody2D _scytheRigidbody2D;

    private bool _scytheReturning, _scytheReturnTimer;

    private NewTimer _timer;
    
    
    void Start()
    {
        _playerData = gameObject.GetComponent<PlayerData>();
        _playerRewired = _playerData.ReturnPlayer();
        _playerAim = gameObject.transform.GetChild(0).gameObject;
        _mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        _spriteRenderer = gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
        _playerAimLimiter = gameObject.transform.GetChild(1).gameObject.GetComponent<PlayerAimLimiter>();
        _timer = gameObject.GetComponent<NewTimer>();
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

        ThrowScythe();
    }

    private void ThrowScythe()
    {
        if (_playerRewired.GetButtonDown("Throw"))
        {
            if (_scythe == null)
            {
                _scytheReturning = false;

                _scythe = Instantiate(ScythePrefab, _spriteRenderer.transform.position, Quaternion.identity);

                _scytheRigidbody2D = _scythe.GetComponent<Rigidbody2D>();

                var dirToPlayer = _scythe.transform.position - gameObject.transform.position;

                _scytheRigidbody2D.AddForce(dirToPlayer * _playerData.PlayerStrength, ForceMode2D.Impulse);

                Debug.Log("ATIREI O SCYTHE! | Direction: " + dirToPlayer + " | Velocity:" + _scytheRigidbody2D.velocity);
            }
            else
            {
                if (!_scytheReturning)
                {
                    var dirToPlayerReversed = gameObject.transform.position - _scythe.transform.position;

                    _scytheRigidbody2D.constraints = RigidbodyConstraints2D.None;

                    _scytheRigidbody2D.isKinematic = false;

                    _scytheRigidbody2D.AddForce(dirToPlayerReversed * (_playerData.PlayerStrength/5), ForceMode2D.Impulse);

                    //FIX de merda já que não faço a mínima ideia do porque a velocidade dele aumenta tanto na volta;

                    _scytheReturning = true;

                    _scythe.GetComponent<CircleCollider2D>().excludeLayers = LayerMask.GetMask("AimLimiter");

                    Debug.Log("SCYTHE VOLTANDO! | Direction: " + dirToPlayerReversed + " | Velocity:" + _scytheRigidbody2D.velocity);
                }
                else
                {
                    if (!_scytheReturnTimer)
                    {
                        _timer.Iniciar(3);
                        _scytheReturnTimer = true;
                    }
                    else
                    {
                        if (_timer.Sinalizar())
                        {
                            _scytheReturning = false;
                            _scytheReturnTimer = false;
                            _timer.Reiniciar();
                        }
                    }
                }
            }
        }
    }

    public void ScytheDidntReturn()
    {
        _scytheReturning = false;
    }
}
