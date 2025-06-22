using UnityEngine;

public class ScytheController : MonoBehaviour
{
    private PlayerAttacksController _playerAttackController;

    void Start()
    {
        _playerAttackController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAttacksController>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("COLLIDIU: " + collision.gameObject.name + " | " + collision.gameObject.layer);

        if (collision.gameObject.layer != 6)
        {
            _playerAttackController.ScytheDidntReturn();
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
