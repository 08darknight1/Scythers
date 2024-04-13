using Rewired;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    public int PlayerID;

    public float PlayerSpeed;
    
    private PlayerData _playerData;

    private Player _playerRewired;

    private Rigidbody2D _rgbd2D;

    private bool _canMove;

    private void Start()
    {
        _playerData = gameObject.GetComponent<PlayerData>();
        _rgbd2D = gameObject.GetComponent<Rigidbody2D>();
        
        _playerData.SetPlayerID(PlayerID);
        _playerRewired = _playerData.ReturnPlayer();
        _canMove = true;
    }

    private void Update()
    {
        if (_canMove)
        {
            var horizontalMov = _playerRewired.GetAxisRaw("Horizontal") * PlayerSpeed;
            var verticalMov = _playerRewired.GetAxisRaw("Vertical") * PlayerSpeed;
            
            _rgbd2D.velocity = new Vector2(horizontalMov, verticalMov);
        }
    }

    public void SetPlayerCanMove(bool valueToSet)
    {
        _canMove = valueToSet;
    }
}
