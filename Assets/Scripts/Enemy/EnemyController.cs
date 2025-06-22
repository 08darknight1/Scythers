using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private EnemyData _enemyData;

    private GameObject _target;

    private Rigidbody2D _rigibody2D;

    void Start()
    {
        _enemyData = gameObject.GetComponent<EnemyData>();
        _target = GameObject.FindGameObjectWithTag("Player");
        _rigibody2D = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_enemyData.EnemyLife > 0)
        {
            var direction = _target.transform.position - gameObject.transform.position;
            
            if(_rigibody2D.velocity.magnitude < _enemyData.EnemyMaximumSpeed)
            {
                _rigibody2D.AddForce(direction * _enemyData.EnemySpeed);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<ScytheController>())
        {
            _target = GameObject.FindGameObjectWithTag("Player");
            _enemyData.EnemyLife -= _target.GetComponent<PlayerData>().PlayerDamage;
        }
    }
}
