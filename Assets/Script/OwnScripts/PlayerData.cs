using Rewired;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    private Player _player;
    private bool _hasSetPlayerID;

    public Player ReturnPlayer()
    {
        if (_hasSetPlayerID)
        {
            return _player;
        }

        return null;
    }

    public void SetPlayerID(int valueToSet)
    {
        _player = ReInput.players.GetPlayer(valueToSet);
        _hasSetPlayerID = true;
    }
}
