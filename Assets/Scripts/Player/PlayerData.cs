using Rewired;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    private Player _player;

    public int PlayerID;

    public int PlayerLife;

    public int PlayerSpeed;

    public int PlayerDamage;

    public int PlayerStrength;

    public void Start()
    {
        _player = ReInput.players.GetPlayer(PlayerID);
    }

    public Player ReturnPlayer()
    {
        return _player;
    }
}
